using System.Collections;

namespace Scarcity
{
    public readonly struct WaveCache : IEnumerable
    {
        public readonly WaveEnemyInfo[] Enemies;

        public WaveEnemyInfo this[int index]
        {
            get => Enemies[index];
            set => Enemies[index] = value;
        }

        public WaveCache(int length)
        {
            Enemies = new WaveEnemyInfo[length];
        }

        public int Length => Enemies.Length;

        public IEnumerator GetEnumerator() => Enemies.GetEnumerator();
    }
}