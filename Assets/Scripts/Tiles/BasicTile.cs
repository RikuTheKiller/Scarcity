using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scarcity
{
    public abstract class BasicTile : TileBase
    {
        public float layer = 0;

        public Tile.ColliderType colliderType = Tile.ColliderType.Grid;

        public static GameObject shadowCasterPrefab;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.Translate(new(0, 0, layer * -0.01f));
            tileData.flags = TileFlags.LockTransform;
            tileData.colliderType = colliderType;

            if (!Application.isPlaying || colliderType == Tile.ColliderType.None) return;

            if (!shadowCasterPrefab)
            {
                shadowCasterPrefab = Resources.Load<GameObject>("Tile Shadow Caster");
            }

            tileData.gameObject = shadowCasterPrefab;
        }
    }
}
