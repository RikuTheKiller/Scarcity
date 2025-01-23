using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scarcity
{
    public class MainGrid : MonoBehaviour
    {
        public static Grid Grid;
        public static Tilemap Tilemap;

        public Grid grid;
        public Tilemap tilemap;

        private void Reset()
        {
            grid = GetComponent<Grid>();
            tilemap = GetComponent<Tilemap>();
        }

        private void Awake()
        {
            Grid = grid;
            Tilemap = tilemap;
        }

        public static Vector3 RoundToCenter(Vector3 point)
        {
            var offset = (Grid.cellSize + Grid.cellGap) * 0.5f;
            return new Vector3(Mathf.Round(point.x - offset.x) + offset.x, Mathf.Round(point.y - offset.y) + offset.y, 0);
        }

        public static Vector3Int GetNearestTile(Vector3 point)
        {
            return Tilemap.WorldToCell(point);
        }
    }
}