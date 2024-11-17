using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scarcity
{
    [CreateAssetMenu(fileName = "Smooth Tile", menuName = "2D/Tiles/Smooth Tile", order = 11)]
    public class SmoothTile : BasicTile
    {
        /// <summary>
        /// Should contain either 16 or 47 sprites depending on if diagonal smoothing is enabled.
        /// </summary>
        public Sprite[] sprites;

        /// <summary>
        /// Whether diagonal smoothing is enabled.
        /// </summary>
        public bool smoothDiagonals = true;

        [HideInInspector]
        public Sprite[] spritesByAdjacency;

        private void OnValidate()
        {
            BuildAdjacencyArray();
        }

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            foreach (var offset in AllOffsets)
            {
                var adjacentPosition = position + offset;

                TileBase adjacentTile = tilemap.GetTile(adjacentPosition);

                if (adjacentTile == null || adjacentTile != this) continue;

                tilemap.RefreshTile(adjacentPosition);
            }

            base.RefreshTile(position, tilemap);
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);

            if (spritesByAdjacency == null) return; // Congrats, you fucked up.

            int adjacencyFlags = 0;

            for (int i = 0; i < Cardinals.Length; i++)
            {
                var adjacentTile = tilemap.GetTile(position + CardinalOffsets[i]);

                if (adjacentTile == null || adjacentTile != this) continue;

                adjacencyFlags |= Cardinals[i];
            }

            if (!smoothDiagonals) return;

            for (int i = 0; i < Diagonals.Length; i++)
            {
                var adjacentCardinals = AdjacentCardinals[i];

                if ((adjacentCardinals & adjacencyFlags) != adjacentCardinals) continue; // Ignores diagonals that aren't adjacent to 2 cardinals, this is what cuts it from 255 to 47.

                var adjacentTile = tilemap.GetTile(position + DiagonalOffsets[i]);

                if (adjacentTile == null || adjacentTile != this) continue;

                adjacencyFlags |= Diagonals[i];
            }

            tileData.sprite = spritesByAdjacency[adjacencyFlags];
        }

        private void BuildAdjacencyArray()
        {
            /*if (sprites.Length != (smoothDiagonals ? 47 : 16))
            {
                Debug.LogError($"SmoothTile \"{name}\" has an incorrectly sized sprite array. Expected {(smoothDiagonals ? 47 : 16)}, but got {sprites.Length}.");
                return;
            }*/

            if (!smoothDiagonals)
            {
                spritesByAdjacency = sprites.ToArray();
                return;
            }

            spritesByAdjacency = new Sprite[256];

            for (int i = 0; i < sprites.Length; i++)
            {
                spritesByAdjacency[AllIndices[i]] = sprites[i];
            }
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
        /// Diagonals adjacent to each cardinal, in the same order as the 'Cardinals' array. This is in nested array form.
        /// </summary>
        public static readonly int[][] AdjacentDiagonals = new int[][]
        {
            new int[] { (int)Dir.Northeast, (int)Dir.Northwest }, // North
            new int[] { (int)Dir.Southeast, (int)Dir.Southwest }, // South
            new int[] { (int)Dir.Northeast, (int)Dir.Southeast }, // East
            new int[] { (int)Dir.Northwest, (int)Dir.Southwest }, // West
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

        /// <summary>
        /// Returns all valid adjacency flags, including diagonals. Don't use this directly, read the cached value from 'AllIndices' instead.
        /// </summary>
        private static int[] GetAllIndices()
        {
            int[] indices = new int[47];

            for (int i = 0; i < 16; i++)
            {
                indices[i] = i; // The first 16 sprites only use cardinals.
            }

            int currentIndex = 16; // Indices start from 0, so even though there are 16 sprites the last index is 15, so we begin at 16.

            for (int i = 16; i <= 255; i++)
            {
                int smoothableDiagonals = 0; // Same logic as in GetTileData(), albeit in a slightly different format.

                for (int i2 = 0; i2 < Diagonals.Length; i2++)
                {
                    int diagonal = Diagonals[i2];

                    if ((i & diagonal) == 0) continue;

                    var adjacentCardinals = AdjacentCardinals[i2];

                    if ((i & adjacentCardinals) != adjacentCardinals) continue;

                    smoothableDiagonals |= diagonal;
                }

                if ((i & smoothableDiagonals) != (i & DiagonalFlags)) continue; // If any of the diagonals in the current flag index are unsmoothable, ignore it.

                indices[currentIndex++] = i; // It's a valid flag index, store it.
            }

            return indices;
        }

        /// <summary>
        /// All valid adjacency flags, including diagonals.
        /// </summary>
        public static readonly int[] AllIndices = GetAllIndices();
    }
}

/* These are the Space Station 13 bitmask smoothing flags.
 * Our use for them? Converting them to an enum.
#define NORTH_JUNCTION NORTH //(1<<0)
#define SOUTH_JUNCTION SOUTH //(1<<1)
#define EAST_JUNCTION EAST  //(1<<2)
#define WEST_JUNCTION WEST  //(1<<3)
#define NORTHEAST_JUNCTION (1<<4)
#define SOUTHEAST_JUNCTION (1<<5)
#define SOUTHWEST_JUNCTION (1<<6)
#define NORTHWEST_JUNCTION (1<<7)
*/
