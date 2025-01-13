using System.Collections;

namespace Scarcity
{
    public readonly struct WaveCacheArray : IEnumerable
    {
        public readonly WaveCache[] Waves;

        public WaveCache this[int index]
        {
            get => Waves[index];
            set => Waves[index] = value;
        }

        public WaveCacheArray(int length)
        {
            Waves = new WaveCache[length];
        }

        public int Length => Waves.Length;

        public IEnumerator GetEnumerator() => Waves.GetEnumerator();
    }
}