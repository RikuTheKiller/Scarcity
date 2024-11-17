using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scarcity
{
    /// <summary>
    /// A very simple tile with a sprite and some settings.
    /// </summary>
    [CreateAssetMenu(fileName = "Simple Tile", menuName = "2D/Tiles/Simple Tile", order = 10)]
    public class SimpleTile : BasicTile
    {
        public Sprite sprite;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);

            tileData.sprite = sprite;
        }
    }
}
