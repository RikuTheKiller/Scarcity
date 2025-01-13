using UnityEngine;

namespace Scarcity
{
    [CreateAssetMenu(fileName = "Simple Wave", menuName = "Wave/Simple Wave")]
    public class SimpleWave : Wave
    {
        public GameObject[] enemies;
        public int count = 1;
        public float delay = 0.1f;

        public override WaveEnemyInfo this[int index]
        {
            get
            {
                if (enemies == null || enemies.Length == 0) return default;
                return new(enemies[index % enemies.Length], delay);
            }
        }

        public override int Count => Mathf.Max(0, enemies.Length * count);
    }
}