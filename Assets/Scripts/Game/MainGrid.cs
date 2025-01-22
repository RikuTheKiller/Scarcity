using UnityEngine;

namespace Scarcity
{
    public class MainGrid : MonoBehaviour
    {
        public static Grid Instance;
        public Grid grid;

        private void Reset()
        {
            grid = GetComponent<Grid>();
        }

        private void Awake()
        {
            Instance = grid;
        }

        public static Vector3 RoundToCell(Vector3 point)
        {
            var offset = (Instance.cellSize + Instance.cellGap) * 0.5f;
            return new Vector3(Mathf.Round(point.x - offset.x) + offset.x, Mathf.Round(point.y - offset.y) + offset.y, 0);
        }
    }
}