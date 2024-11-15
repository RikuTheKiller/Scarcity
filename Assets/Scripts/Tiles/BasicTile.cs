using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scarcity
{
    public abstract class BasicTile : TileBase
    {
        public float layer = 0;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.Translate(new(0, 0, -layer));
            tileData.flags = TileFlags.LockTransform;
        }
    }
}
