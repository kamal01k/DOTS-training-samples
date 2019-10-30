using Unity.Collections;
using Unity.Mathematics;
using Unity.Entities;

public struct Tile
{
    public eTileType TileType
    {
        get
        {
            return (eTileType)(m_PackedData & 0xff);
        }
        set
        {
            m_PackedData &= 0xffffff00;
            m_PackedData |= (uint)value;
        }
    }

    public eColor Color
    {
        get
        {
            return (eColor)((m_PackedData >> 8) & 0xff);
        }
        set
        {
            m_PackedData &= 0xffff00ff;
            m_PackedData |= ((uint)value << 8);
        }
    }

    public eDirection Direction
    {
        get
        {
            return (eDirection)((m_PackedData >> 16) & 0xff);
        }
        set
        {
            m_PackedData &= 0xff00ffff;
            m_PackedData |= ((uint)value << 16);
        }
    }

    public bool HasWall(eDirection direction)
    {
        int dirAsInt = (int)direction;
        uint shifted = (uint)(1 << (dirAsInt + 24));
        return (m_PackedData & shifted) != 0;
    }

    public void SetWall(eDirection direction, bool wall)
    {
        uint dirAsInt = (uint)direction;
        if (wall)
        {
            int dirAsint = (int)direction + 24;
            var shifted = ((uint)1) << ((byte)dirAsInt);
            m_PackedData |= shifted;
        }
        else
        {
            int dirAsint = (int)direction + 24;
            var shifted = ((uint)1) << ((byte)dirAsInt);
            m_PackedData &= ~shifted;
        }
    }

    // first byte is eTileType
    // second byte is eColor
    // third byte is eDirection
    // fourth byte has packed bits for whether there's a wall in each direction
    private uint m_PackedData;
}

public class BoardSystem : ComponentSystem
{
    public static int2 ConvertWorldToTileCoordinates(float3 position)
    {
        return new int2((int)(position.x - 0.5f * (float)k_Width + 0.5f), (int)(position.z - 0.5f * (float)k_Height + 0.5f));
    }

    const int k_Width = 13;
    const int k_Height = 13;

    NativeArray<Tile> m_Board;

    public Tile this[int x, int y]
    {
        get
        {
            return m_Board[y * k_Width + x];
        }
        set
        {
            m_Board[y * k_Width + x] = value;
        }
    }

    protected override void OnCreate()
    {
        m_Board = new NativeArray<Tile>(k_Width * k_Height, Allocator.Persistent);
        for (int y = 0; y < k_Height; ++y)
        {
            for (int x = 0; x < k_Width; ++x)
            {
                int index = y * k_Width + x;
                var tile = m_Board[index];

                tile.TileType = eTileType.Blank;
                m_Board[index] = tile;

                if (x == 0)
                    m_Board[index].SetWall(eDirection.West, true);
                else if (x == k_Width - 1)
                    m_Board[index].SetWall(eDirection.East, true);
            }

            int indexY = y * k_Width;
            var tileY = m_Board[indexY];
            if (y == 0)
                m_Board[indexY].SetWall(eDirection.South, true);
            else if (y == k_Height - 1)
                m_Board[indexY].SetWall(eDirection.North, true);
        }
    }

    protected override void OnDestroy()
    {
        m_Board.Dispose();
    }

    protected override void OnUpdate()
    {
    }
}