using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scarcity
{
    [ExecuteAlways]
    public class ShadowTilemap : MonoBehaviour
    {
        public Tilemap referenceTilemap;
        public Tilemap shadowTilemap;
        public TileBase shadowTile;

        private void Reset()
        {
            shadowTilemap = GetComponent<Tilemap>();
        }

        private void OnEnable()
        {
            UpdateTiles();

            Tilemap.tilemapPositionsChanged += OnTilemapPositionsChanged;
        }

        private void OnDisable()
        {
            Tilemap.tilemapPositionsChanged -= OnTilemapPositionsChanged;
        }

        public void UpdateTiles()
        {
            if (!referenceTilemap) return;

            foreach (var position in referenceTilemap.cellBounds.allPositionsWithin)
            {
                UpdateTile(position, referenceTilemap.GetTile(position));
            }
        }

        public void UpdateTile(Vector3Int position, TileBase tile)
        {
            bool shadow = tile != null && tile is BasicTile basicTile && basicTile.colliderType != Tile.ColliderType.None;

            shadowTilemap.SetTile(position, shadow ? shadowTile : null);
        }

        private void OnTilemapPositionsChanged(Tilemap tilemap, NativeArray<Vector3Int> positions)
        {
            if (tilemap != referenceTilemap) return;

            foreach (var position in positions)
            {
                UpdateTile(position, tilemap.GetTile(position));
            }
        }
    }
}
