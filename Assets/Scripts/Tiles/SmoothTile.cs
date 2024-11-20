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

        public SmoothingGroup smoothWithGroup = SmoothingGroup.Self;
        public SmoothingGroup smoothGroup = 0;

        private void OnValidate()
        {
            BuildAdjacencyArray();
        }

        public override void GetVisualTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetVisualTileData(position, tilemap, ref tileData);

            if (spritesByAdjacency == null) return; // Congrats, you fucked up.

            int adjacencyFlags = 0;

            for (int i = 0; i < Cardinals.Length; i++)
            {
                var adjacentTile = tilemap.GetTile(position + CardinalOffsets[i]);

                if (!CanSmoothWith(adjacentTile)) continue;

                adjacencyFlags |= Cardinals[i];
            }

            if (!smoothDiagonals)
            {
                tileData.sprite = spritesByAdjacency[adjacencyFlags];
                return;
            }

            for (int i = 0; i < Diagonals.Length; i++)
            {
                var adjacentCardinals = AdjacentCardinals[i];

                if ((adjacentCardinals & adjacencyFlags) != adjacentCardinals) continue; // Ignores diagonals that aren't adjacent to 2 cardinals, this is what cuts it from 255 to 47.

                var adjacentTile = tilemap.GetTile(position + DiagonalOffsets[i]);

                if (!CanSmoothWith(adjacentTile)) continue;

                adjacencyFlags |= Diagonals[i];
            }

            tileData.sprite = spritesByAdjacency[adjacencyFlags];
        }

        public bool CanSmoothWith(TileBase otherTile)
        {
            if (otherTile == this && (smoothWithGroup & SmoothingGroup.Self) != 0) return true;
            return otherTile != null && otherTile is SmoothTile smoothTile && ((smoothWithGroup & smoothTile.smoothGroup) != 0);
        }

        private void BuildAdjacencyArray()
        {
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

        [Serializable, Flags]
        public enum SmoothingGroup
        {
            Self = 1 << 1,
            Grass = 1 << 2,
            Path = 1 << 3,
            EdgeWall = 1 << 4,
        }
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
