using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[ExecuteAlways]
public class DualGrid : MonoBehaviour
{
    public Tilemap placeholderTilemap;
    public Tilemap visualTilemap;

    public List<Tile> placeholderTiles;
    public List<Tile> visualTiles;

    public Tile[] tilesByAdjacency = new Tile[16];

    /// <summary>
    /// Here, 1 is the primary tile (grass) and 0 is the secondary tile (dirt), this is based on the order of the tiles in the palette.
    /// </summary>
    public static readonly int[] tileOrder = new int[]
    {
        0b1000,
        0b0101,
        0b1110,
        0b1100,
        0b0110,
        0b1101,
        0b1111,
        0b1011,
        0b0001,
        0b0011,
        0b0111,
        0b1010,
        0b0000,
        0b0100,
        0b1001,
        0b0010,
    };

    private void Awake()
    {
        UpdateAllTiles();
    }

    private void OnEnable()
    {
        Tilemap.tilemapPositionsChanged += OnTilemapPositionsChanged;
    }

    private void OnDisable()
    {
        Tilemap.tilemapPositionsChanged -= OnTilemapPositionsChanged;
    }

    private void OnValidate()
    {
        CreateDict();
    }

    private void CreateDict()
    {
        for (int i = 0; i < visualTiles.Count; i++)
        {
            tilesByAdjacency[tileOrder[i]] = visualTiles[i];
        }
    }

    /// <summary>
    /// Gets the placeholder tile adjacency of the visual tile at the given position. Returns -1 if it has no adjacent tiles.
    /// </summary>
    private int GetAdjacency(Vector3Int position)
    {
        BoundsInt placeholderBounds = new(position.x - 1, position.y - 1, 0, 2, 2, 1);

        TileBase[] tiles = new TileBase[4];

        placeholderTilemap.GetTilesBlockNonAlloc(placeholderBounds, tiles);

        bool exists = false;

        foreach (var tile in tiles)
        {
            if (tile != null)
            {
                exists = true;
                break;
            }
        }

        if (!exists)
        {
            return -1;
        }

        int adjacency = 0;

        Tile primaryTile = placeholderTiles[1];

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] == primaryTile)
            {
                adjacency |= 1 << tiles.Length - 1 - i;
            }
        }

        return adjacency;
    }

    /// <summary>
    /// Updates a visual tile at the given position.
    /// </summary>
    private void UpdateVisualTile(Vector3Int position)
    {
        int adjacency = GetAdjacency(position);

        visualTilemap.SetTile(position, adjacency == -1 ? null : tilesByAdjacency[adjacency]);
    }

    /// <summary>
    /// Updates the visual tile neighbors of a placeholder tile.
    /// </summary>
    private void UpdatePlaceholderTile(Vector3Int position)
    {
        for (int x = position.x; x < position.x + 2; x++)
        {
            for (int y = position.y; y < position.y + 2; y++)
            {
                UpdateVisualTile(new(x, y));
            }
        }
    }

    /// <summary>
    /// Updates all visual tiles. Very inefficient.
    /// </summary>
    private void UpdateAllTiles()
    {
        BoundsInt bounds = placeholderTilemap.cellBounds;

        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                UpdateVisualTile(new Vector3Int(x, y));
            }
        }
    }

    private void OnTilemapPositionsChanged(Tilemap tilemap, NativeArray<Vector3Int> positions)
    {
        if (tilemap != placeholderTilemap) return;

        Undo.RecordObject(visualTilemap, "Sync Visual Tilemap");

        foreach (var position in positions)
        {
            UpdatePlaceholderTile(position);
        }
    }
}
