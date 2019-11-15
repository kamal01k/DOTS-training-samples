//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     TextTransform Samples/Packages/com.unity.entities/Unity.Entities/UnsafeList.tt
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Entities
{
    [DebuggerTypeProxy(typeof(UnsafeIntListDebugView))]
    internal unsafe struct UnsafeIntList
    {
        [NativeDisableUnsafePtrRestriction]
        public readonly int* Ptr;
        public readonly int Length;
        public readonly int Capacity;
        public readonly Allocator Allocator;

        private ref UnsafeList ListData { get { return ref *(UnsafeList*)UnsafeUtility.AddressOf(ref this); } }

        public unsafe UnsafeIntList(int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) { Ptr = null; Length = 0; Capacity = 0; Allocator = Allocator.Invalid; ListData = new UnsafeList(UnsafeUtility.SizeOf<int>(), UnsafeUtility.AlignOf<int>(), initialCapacity, allocator, options); }
        public void Dispose() { ListData.Dispose(); }
        public JobHandle Dispose(JobHandle inputDeps) { return ListData.Dispose(inputDeps); }
        public void Clear() { ListData.Clear(); }
        public void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) { ListData.Resize<int>(length, options); }
        public void SetCapacity(int capacity) { ListData.SetCapacity<int>(capacity); }
        public int IndexOf(int value) { return ListData.IndexOf(value); }
        public bool Contains(int value) { return ListData.Contains(value); }
        public void Add(int value) { ListData.Add(value); }
        public void AddRange(UnsafeIntList src) { ListData.AddRange<int>(src.ListData); }
        public void RemoveAtSwapBack(int index) { ListData.RemoveAtSwapBack<int>(index); }
        public ParallelReader AsParallelReader() { return new ParallelReader(Ptr, Length); }

        public unsafe struct ParallelReader
        {
            public readonly int* Ptr;
            public readonly int Length;

            public ParallelReader(int* ptr, int length) { Ptr = ptr; Length = length; }
            public int IndexOf(int value) { for (int i = Length - 1; i >= 0; --i) { if (Ptr[i] == value) { return i; } } return -1; }
            public bool Contains(int value) { return IndexOf(value) != -1; }
        }
    }

    sealed class UnsafeIntListDebugView
    {
        private UnsafeIntList m_ListData;

        public UnsafeIntListDebugView(UnsafeIntList listData)
        {
            m_ListData = listData;
        }

        public unsafe int[] Items
        {
            get
            {
                var result = new int[m_ListData.Length];
                var ptr    = m_ListData.Ptr;

                for (int i = 0, num = result.Length; i < num; ++i)
                {
                    result[i] = ptr[i];
                }

                return result;
            }
        }
    }

    [DebuggerTypeProxy(typeof(UnsafeUintListDebugView))]
    internal unsafe struct UnsafeUintList
    {
        [NativeDisableUnsafePtrRestriction]
        public readonly uint* Ptr;
        public readonly int Length;
        public readonly int Capacity;
        public readonly Allocator Allocator;

        private ref UnsafeList ListData { get { return ref *(UnsafeList*)UnsafeUtility.AddressOf(ref this); } }

        public unsafe UnsafeUintList(int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) { Ptr = null; Length = 0; Capacity = 0; Allocator = Allocator.Invalid; ListData = new UnsafeList(UnsafeUtility.SizeOf<uint>(), UnsafeUtility.AlignOf<uint>(), initialCapacity, allocator, options); }
        public void Dispose() { ListData.Dispose(); }
        public JobHandle Dispose(JobHandle inputDeps) { return ListData.Dispose(inputDeps); }
        public void Clear() { ListData.Clear(); }
        public void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) { ListData.Resize<uint>(length, options); }
        public void SetCapacity(int capacity) { ListData.SetCapacity<uint>(capacity); }
        public int IndexOf(uint value) { return ListData.IndexOf(value); }
        public bool Contains(uint value) { return ListData.Contains(value); }
        public void Add(uint value) { ListData.Add(value); }
        public void AddRange(UnsafeUintList src) { ListData.AddRange<uint>(src.ListData); }
        public void RemoveAtSwapBack(int index) { ListData.RemoveAtSwapBack<uint>(index); }
        public ParallelReader AsParallelReader() { return new ParallelReader(Ptr, Length); }

        public unsafe struct ParallelReader
        {
            public readonly uint* Ptr;
            public readonly int Length;

            public ParallelReader(uint* ptr, int length) { Ptr = ptr; Length = length; }
            public int IndexOf(uint value) { for (int i = Length - 1; i >= 0; --i) { if (Ptr[i] == value) { return i; } } return -1; }
            public bool Contains(uint value) { return IndexOf(value) != -1; }
        }
    }

    sealed class UnsafeUintListDebugView
    {
        private UnsafeUintList m_ListData;

        public UnsafeUintListDebugView(UnsafeUintList listData)
        {
            m_ListData = listData;
        }

        public unsafe uint[] Items
        {
            get
            {
                var result = new uint[m_ListData.Length];
                var ptr    = m_ListData.Ptr;

                for (int i = 0, num = result.Length; i < num; ++i)
                {
                    result[i] = ptr[i];
                }

                return result;
            }
        }
    }

    [DebuggerTypeProxy(typeof(UnsafeChunkPtrListDebugView))]
    internal unsafe struct UnsafeChunkPtrList
    {
        [NativeDisableUnsafePtrRestriction]
        public readonly Chunk** Ptr;
        public readonly int Length;
        public readonly int Capacity;
        public readonly Allocator Allocator;

        private ref UnsafePtrList ListData { get { return ref *(UnsafePtrList*)UnsafeUtility.AddressOf(ref this); } }

        public unsafe UnsafeChunkPtrList(Chunk** ptr, int length) { Ptr = null; Length = 0; Capacity = 0; Allocator = Allocator.Invalid; ListData = new UnsafePtrList((void**)ptr, length); }
        public unsafe UnsafeChunkPtrList(int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) { Ptr = null; Length = 0; Capacity = 0; Allocator = Allocator.Invalid; ListData = new UnsafePtrList(initialCapacity, allocator, options); }
        public void Dispose() { ListData.Dispose(); }
        public JobHandle Dispose(JobHandle inputDeps) { return ListData.Dispose(inputDeps); }
        public void Clear() { ListData.Clear(); }
        public void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) { ListData.Resize(length, options); }
        public void SetCapacity(int capacity) { ListData.SetCapacity(capacity); }
        public int IndexOf(Chunk* value) { return ListData.IndexOf(value); }
        public bool Contains(Chunk* value) { return ListData.Contains(value); }
        public void Add(Chunk* value) { ListData.Add(value); }
        public void AddRange(UnsafeChunkPtrList src) { ListData.AddRange(src.ListData); }
        public void RemoveAtSwapBack(int index) { ListData.RemoveAtSwapBack(index); }

        public unsafe struct ParallelReader
        {
            public readonly Chunk** Ptr;
            public readonly int Length;

            public ParallelReader(Chunk** ptr, int length) { Ptr = ptr; Length = length; }
            public int IndexOf(Chunk* value) { for (int i = Length - 1; i >= 0; --i) { if (Ptr[i] == value) { return i; } } return -1; }
            public bool Contains(Chunk* value) { return IndexOf(value) != -1; }
        }
    }

    sealed class UnsafeChunkPtrListDebugView
    {
        private UnsafeChunkPtrList m_ListData;

        public UnsafeChunkPtrListDebugView(UnsafeChunkPtrList listData)
        {
            m_ListData = listData;
        }

        public unsafe ArchetypeChunk[] Items
        {
            get
            {
                var result = new ArchetypeChunk[m_ListData.Length];
                var ptr    = m_ListData.Ptr;

                for (int i = 0, num = result.Length; i < num; ++i)
                {
                    result[i] = *(ArchetypeChunk*)ptr[i];
                }

                return result;
            }
        }
    }
    [DebuggerTypeProxy(typeof(UnsafeArchetypePtrListDebugView))]
    internal unsafe struct UnsafeArchetypePtrList
    {
        [NativeDisableUnsafePtrRestriction]
        public readonly Archetype** Ptr;
        public readonly int Length;
        public readonly int Capacity;
        public readonly Allocator Allocator;

        private ref UnsafePtrList ListData { get { return ref *(UnsafePtrList*)UnsafeUtility.AddressOf(ref this); } }

        public unsafe UnsafeArchetypePtrList(Archetype** ptr, int length) { Ptr = null; Length = 0; Capacity = 0; Allocator = Allocator.Invalid; ListData = new UnsafePtrList((void**)ptr, length); }
        public unsafe UnsafeArchetypePtrList(int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) { Ptr = null; Length = 0; Capacity = 0; Allocator = Allocator.Invalid; ListData = new UnsafePtrList(initialCapacity, allocator, options); }
        public void Dispose() { ListData.Dispose(); }
        public JobHandle Dispose(JobHandle inputDeps) { return ListData.Dispose(inputDeps); }
        public void Clear() { ListData.Clear(); }
        public void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) { ListData.Resize(length, options); }
        public void SetCapacity(int capacity) { ListData.SetCapacity(capacity); }
        public int IndexOf(Archetype* value) { return ListData.IndexOf(value); }
        public bool Contains(Archetype* value) { return ListData.Contains(value); }
        public void Add(Archetype* value) { ListData.Add(value); }
        public void AddRange(UnsafeArchetypePtrList src) { ListData.AddRange(src.ListData); }
        public void RemoveAtSwapBack(int index) { ListData.RemoveAtSwapBack(index); }

        public unsafe struct ParallelReader
        {
            public readonly Archetype** Ptr;
            public readonly int Length;

            public ParallelReader(Archetype** ptr, int length) { Ptr = ptr; Length = length; }
            public int IndexOf(Archetype* value) { for (int i = Length - 1; i >= 0; --i) { if (Ptr[i] == value) { return i; } } return -1; }
            public bool Contains(Archetype* value) { return IndexOf(value) != -1; }
        }
    }

    sealed class UnsafeArchetypePtrListDebugView
    {
        private UnsafeArchetypePtrList m_ListData;

        public UnsafeArchetypePtrListDebugView(UnsafeArchetypePtrList listData)
        {
            m_ListData = listData;
        }

        public unsafe EntityArchetype[] Items
        {
            get
            {
                var result = new EntityArchetype[m_ListData.Length];
                var ptr    = m_ListData.Ptr;

                for (int i = 0, num = result.Length; i < num; ++i)
                {
                    result[i] = *(EntityArchetype*)ptr[i];
                }

                return result;
            }
        }
    }
    [DebuggerTypeProxy(typeof(UnsafeEntityQueryDataPtrListDebugView))]
    internal unsafe struct UnsafeEntityQueryDataPtrList
    {
        [NativeDisableUnsafePtrRestriction]
        public readonly EntityQueryData** Ptr;
        public readonly int Length;
        public readonly int Capacity;
        public readonly Allocator Allocator;

        private ref UnsafePtrList ListData { get { return ref *(UnsafePtrList*)UnsafeUtility.AddressOf(ref this); } }

        public unsafe UnsafeEntityQueryDataPtrList(EntityQueryData** ptr, int length) { Ptr = null; Length = 0; Capacity = 0; Allocator = Allocator.Invalid; ListData = new UnsafePtrList((void**)ptr, length); }
        public unsafe UnsafeEntityQueryDataPtrList(int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) { Ptr = null; Length = 0; Capacity = 0; Allocator = Allocator.Invalid; ListData = new UnsafePtrList(initialCapacity, allocator, options); }
        public void Dispose() { ListData.Dispose(); }
        public JobHandle Dispose(JobHandle inputDeps) { return ListData.Dispose(inputDeps); }
        public void Clear() { ListData.Clear(); }
        public void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) { ListData.Resize(length, options); }
        public void SetCapacity(int capacity) { ListData.SetCapacity(capacity); }
        public int IndexOf(EntityQueryData* value) { return ListData.IndexOf(value); }
        public bool Contains(EntityQueryData* value) { return ListData.Contains(value); }
        public void Add(EntityQueryData* value) { ListData.Add(value); }
        public void AddRange(UnsafeEntityQueryDataPtrList src) { ListData.AddRange(src.ListData); }
        public void RemoveAtSwapBack(int index) { ListData.RemoveAtSwapBack(index); }

        public unsafe struct ParallelReader
        {
            public readonly EntityQueryData** Ptr;
            public readonly int Length;

            public ParallelReader(EntityQueryData** ptr, int length) { Ptr = ptr; Length = length; }
            public int IndexOf(EntityQueryData* value) { for (int i = Length - 1; i >= 0; --i) { if (Ptr[i] == value) { return i; } } return -1; }
            public bool Contains(EntityQueryData* value) { return IndexOf(value) != -1; }
        }
    }

    sealed class UnsafeEntityQueryDataPtrListDebugView
    {
        private UnsafeEntityQueryDataPtrList m_ListData;

        public UnsafeEntityQueryDataPtrListDebugView(UnsafeEntityQueryDataPtrList listData)
        {
            m_ListData = listData;
        }

        public unsafe EntityQueryData*[] Items
        {
            get
            {
                var result = new EntityQueryData*[m_ListData.Length];
                var ptr    = m_ListData.Ptr;

                for (int i = 0, num = result.Length; i < num; ++i)
                {
                    result[i] = *(EntityQueryData**)ptr[i];
                }

                return result;
            }
        }
    }
}