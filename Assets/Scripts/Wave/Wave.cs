using UnityEngine;

namespace Scarcity
{
    public abstract class Wave : ScriptableObject
    {
        public abstract WaveEnemyInfo this[int index] { get; }
        public abstract int Count { get; }

        /// <summary>
        /// Returns a flattened array of all of the entities this spawns.
        /// </summary>
        public WaveCache Cache()
        {
            var result = new WaveCache(Count);

            for (int i = 0; i < Count; i++)
            {
                result[i] = this[i];
            }

            return result;
        }

        /// <summary>
        /// Returns an array of nested cache arrays. Only different from Cache() for wave types that can contain other waves, like CompositeWave.
        /// </summary>
        public virtual WaveCacheArray CacheSegmented()
        {
            var result = new WaveCacheArray(1);
            result[0] = Cache();
            return result;
        }
    }
}
