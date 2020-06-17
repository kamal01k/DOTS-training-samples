﻿using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;

namespace HighwayRacer
{
    [UpdateAfter(typeof(UnblockedCarSys))]
    public class BlockedCarSys : SystemBase
    {
        private NativeArray<OtherCars> selection; // the OtherCar segments to compare against a particular car

        const int nSegments = RoadInit.nSegments;
        const int nLanes = RoadInit.nLanes;
        const float minDist = RoadInit.minDist;
        const float minMergeSegmentPos = RoadInit.minMergeSegmentPos;
        const float decelerationRate = RoadInit.decelerationRate;
        const float accelerationRate = RoadInit.accelerationRate;

        protected override void OnCreate()
        {
            base.OnCreate();

            selection = new NativeArray<OtherCars>(2, Allocator.Persistent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            selection.Dispose();
        }

        private struct Util
        {
            // todo: account for speed of adjacent car ahead and car behind relative to this car
            public static bool canMerge(float pos, int destLane, int segment, NativeArray<OtherCars> otherCars, float trackLength)
            {
                var laneBaseIdx = destLane * nSegments;
                
                var idx = laneBaseIdx + segment;
                var adjacentLane = otherCars[idx];

                var wrapAround = (segment == nSegments - 1);
                
                idx = laneBaseIdx + (wrapAround ? 0 : segment + 1);
                var adjacentLaneNextSegment = otherCars[idx];

                // find pos and speed of closest car ahead and closest car behind 
                var closestAheadPos = float.MaxValue;
                var closestAheadSpeed = 0.0f;

                var closestBehindPos = float.MinValue;
                var closestBehindSpeed = 0.0f;

                var posSegment = adjacentLane.positions;
                var speedSegment = adjacentLane.speeds;
                
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < posSegment.Length; j++)
                    {
                        var otherPos = posSegment[j].Val + (wrapAround && i == 1 ? trackLength : 0);
                        var otherSpeed = speedSegment[j];

                        if (otherPos < closestAheadPos &&
                            otherPos > pos) // found a car ahead that's closer than previous closest
                        {
                            closestAheadPos = otherPos;
                            closestAheadSpeed = otherSpeed.Val;
                        }
                        else if (otherPos > closestBehindPos &&
                                 otherPos <= pos) // found a car behind (or equal) that's closer than previous closest
                        {
                            closestBehindPos = otherPos;
                            closestBehindSpeed = otherSpeed.Val;
                        }
                    }

                    posSegment = adjacentLaneNextSegment.positions;
                    speedSegment = adjacentLaneNextSegment.speeds;
                }

                return false;
                //return true;
            }
        }


        protected override void OnUpdate()
        {
            var trackLength = RoadInit.trackLength;
            var roadSegments = RoadInit.roadSegments;

            var selection = this.selection;
            var otherCars = World.GetExistingSystem<CarsByLaneSegmentSys>().otherCars;

            var dt = Time.DeltaTime;

            // make sure we don't hit next car ahead, and trigger overtake state
            var ecb = new EntityCommandBuffer(Allocator.TempJob);

            Entities.WithAll<Blocked>().ForEach((Entity ent, ref TargetSpeed targetSpeed, ref Speed speed, ref Lane lane, in TrackPos trackPos,
                in TrackSegment trackSegment, in BlockedDist blockedDist, in UnblockedSpeed unblockedSpeed) =>
            {
                var laneBaseIdx = lane.Val * nSegments;

                var idx = laneBaseIdx + trackSegment.Val;
                selection[0] = otherCars[idx];

                // next
                idx = laneBaseIdx + ((trackSegment.Val == nSegments - 1) ? 0 : trackSegment.Val + 1);
                selection[1] = otherCars[idx];

                // find pos and speed of closest car ahead
                var closestSpeed = 0.0f;
                var closestPos = float.MaxValue;
                for (int i = 0; i < 2; i++)
                {
                    var posSegment = selection[i].positions;
                    var speedSegment = selection[i].speeds;

                    var wrapAround = (trackSegment.Val == nSegments - 1) && i == 1;

                    for (int j = 0; j < posSegment.Length; j++)
                    {
                        var otherPos = posSegment[j].Val + (wrapAround ? trackLength : 0);
                        var otherSpeed = speedSegment[j];

                        if (otherPos < closestPos &&
                            otherPos > trackPos.Val) // found a car ahead closer than previous closest
                        {
                            closestPos = otherPos;
                            closestSpeed = otherSpeed.Val;
                        }
                    }
                }

                if (closestPos != float.MaxValue)
                {
                    var dist = closestPos - trackPos.Val;
                    if (dist <= blockedDist.Val &&
                        speed.Val > closestSpeed) // car is still blocked ahead in lane
                    {
                        var closeness = (dist - minDist) / (blockedDist.Val - minDist); // 0 is max closeness, 1 is min

                        // closer we get within minDist of leading car, the closer we match speed
                        const float fudge = 2.0f;
                        var newSpeed = math.lerp(closestSpeed, speed.Val + fudge, closeness);
                        if (newSpeed < speed.Val)
                        {
                            speed.Val = newSpeed;
                        }
                        
                        // if pos is too close to start of its segment, can't merge

                        var segmentPos = trackPos.Val - roadSegments[trackSegment.Val].Threshold; 
                        if (segmentPos < minMergeSegmentPos)
                        {
                            return;
                        }

                        // look for opening on left
                        if (lane.Val < nLanes - 1)
                        {
                            var leftLaneIdx = lane.Val + 1;
                            if (Util.canMerge(trackPos.Val, leftLaneIdx, trackSegment.Val, otherCars, trackLength))
                            {
                                ecb.AddComponent<MergingLeft>(ent);
                                ecb.RemoveComponent<Blocked>(ent);
                                lane.Val = (byte) leftLaneIdx;
                            }
                        }

                        // look for opening on right
                        if (lane.Val > 0)
                        {
                            var rightLaneIdx = lane.Val - 1;
                            if (Util.canMerge(trackPos.Val, rightLaneIdx, trackSegment.Val, otherCars, trackLength))
                            {
                                ecb.AddComponent<MergingRight>(ent);
                                ecb.RemoveComponent<Blocked>(ent);
                                lane.Val = (byte) rightLaneIdx;
                            }
                        }

                        return;
                    }
                }

                ecb.RemoveComponent<Blocked>(ent);

                targetSpeed.Val = unblockedSpeed.Val;

                if (targetSpeed.Val < speed.Val)
                {
                    speed.Val -= decelerationRate * dt;
                    if (speed.Val < targetSpeed.Val)
                    {
                        speed.Val = targetSpeed.Val;
                    }
                }
                else if (targetSpeed.Val > speed.Val)
                {
                    speed.Val += accelerationRate * dt;
                    if (speed.Val > targetSpeed.Val)
                    {
                        speed.Val = targetSpeed.Val;
                    }
                }
            }).Run();

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}