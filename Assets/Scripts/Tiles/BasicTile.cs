using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scarcity
{
    public abstract class BasicTile : TileBase
    {
        public float layer = 0;

        public Tile.ColliderType colliderType = Tile.ColliderType.Grid;

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            foreach (var offset in AllOffsets)
            {
                var adjacentPosition = position + offset;

                TileBase adjacentTile = tilemap.GetTile(adjacentPosition);

                if (adjacentTile == null) continue;

                tilemap.RefreshTile(adjacentPosition);
            }

            base.RefreshTile(position, tilemap);
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            bool visible = IsVisible(position, tilemap, ref tileData);

            if (Application.isPlaying && !IsVisible(position, tilemap, ref tileData)) return;

            GetVisualTileData(position, tilemap, ref tileData);
        }

        public virtual void GetVisualTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.Translate(new(0, 0, layer * -0.01f));
            tileData.flags = TileFlags.LockTransform;
            tileData.colliderType = colliderType;
        }

        public virtual bool IsVisible(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            foreach (var offset in AllOffsets)
            {
                TileBase adjacentTile = tilemap.GetTile(position + offset);

                if (adjacentTile == null || adjacentTile is not BasicTile basic || basic.colliderType == Tile.ColliderType.None) return true;
            }

            return false;
        }

        [Flags]
        public enum Dir
        {
            None = 0,
            North = 1 << 0,
            South = 1 << 1,
            East = 1 << 2,
            West = 1 << 3,
            Northeast = 1 << 4,
            Southeast = 1 << 5,
            Southwest = 1 << 6,
            Northwest = 1 << 7,
        }

        public static readonly int[] Cardinals = new int[]
        {
            (int)Dir.North,
            (int)Dir.South,
            (int)Dir.East,
            (int)Dir.West,
        };

        public static readonly int[] Diagonals = new int[]
        {
            (int)Dir.Northeast,
            (int)Dir.Southeast,
            (int)Dir.Southwest,
            (int)Dir.Northwest,
        };

        /// <summary>
        /// Vector3Int equivalent for each cardinal, in the same order as the 'Cardinals' array.
        /// </summary>
        public static readonly Vector3Int[] CardinalOffsets = new Vector3Int[]
        {
            Vector3Int.up, // North
            Vector3Int.down, // South
            Vector3Int.right, // East
            Vector3Int.left, // West
        };

        /// <summary>
        /// Vector3Int equivalent for each diagonal, in the same order as the 'Diagonals' array.
        /// </summary>
        public static readonly Vector3Int[] DiagonalOffsets = new Vector3Int[]
        {
            Vector3Int.up + Vector3Int.right, // Northeast
            Vector3Int.down + Vector3Int.right, // Southeast
            Vector3Int.down + Vector3Int.left, // Southwest
            Vector3Int.up + Vector3Int.left, // Northwest
        };

        /// <summary>
        /// Combination of 'CardinalOffsets' and 'DiagonalOffsets' in that order.
        /// </summary>
        public static readonly Vector3Int[] AllOffsets = CardinalOffsets.Concat(DiagonalOffsets).ToArray();

        /// <summary>
        /// Cardinals adjacent to each diagonal, in the same order as the 'Diagonals' array.
        /// </summary>
        public static readonly int[] AdjacentCardinals = new int[]
        {
            (int)(Dir.North | Dir.East), // Northeast
            (int)(Dir.South | Dir.East), // Southeast
            (int)(Dir.South | Dir.West), // Southwest
            (int)(Dir.North | Dir.West), // Northwest
        };

        /// <summary>
        /// All cardinal bitflags in one value.
        /// </summary>
        public const int CardinalFlags = (int)(Dir.North | Dir.South | Dir.East | Dir.West);

        /// <summary>
        /// All diagonal bitflags in one value.
        /// </summary>
        public const int DiagonalFlags = (int)(Dir.Northeast | Dir.Southeast | Dir.Southwest | Dir.Northwest);
    }
}
