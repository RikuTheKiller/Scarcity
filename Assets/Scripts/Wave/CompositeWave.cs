using UnityEngine;

namespace Scarcity
{
    [CreateAssetMenu(fileName = "Composite Wave", menuName = "Wave/Composite Wave")]
    public class CompositeWave : Wave
    {
        public Wave[] waves;

        public override WaveEnemyInfo this[int index]
        {
            get
            {
                if (index < 0) return default;

                var last = 0;

                foreach (var wave in waves)
                {
                    last += wave.Count;
                    if (last <= index) continue;
                    return wave[index - last + wave.Count];
                }

                return default;
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

        public override WaveCacheArray ToCacheArray()
        {
            var result = new WaveCacheArray(waves.Length);

            for (int i = 0; i < waves.Length; i++)
            {
                result[i] = waves[i].ToCache();
            }

            return result;
        }
    }
}