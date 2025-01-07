using UnityEngine;

namespace Scarcity
{
    [CreateAssetMenu(fileName = "Composite Wave", menuName = "Wave/Composite Wave")]
    public class CompositeWave : Wave
    {
        public Wave[] waves;

        public override WaveInfo this[int index]
        {
            get
            {
                if (index < 0) return null;

                var last = 0;

                foreach (var wave in waves)
                {
                    last += wave.Count;
                    if (last <= index) continue;
                    return wave[index - last + wave.Count];
                }

                return null;
            }
        }

        public override int Count
        {
            get
            {
                var count = 0;

                foreach (var wave in waves)
                {
                    count += wave.Count;
                }

                return count;
            }
        }

        public override WaveInfo[][] CacheSegmented()
        {
            var result = new WaveInfo[waves.Length][];

            for (int i = 0; i < waves.Length; i++)
            {
                result[i] = waves[i].Cache();
            }

            return result;
        }
    }
}