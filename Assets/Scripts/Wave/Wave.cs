using UnityEngine;

namespace Scarcity
{
    public abstract class Wave : ScriptableObject
    {
        public abstract WaveInfo this[int index] { get; }
        public abstract int Count { get; }

        /// <summary>
        /// Returns a flattened array of all of the entities this spawns.
        /// </summary>
        public WaveInfo[] Cache()
        {
            var result = new WaveInfo[Count];

            for (int i = 0; i < Count; i++)
            {
                result[i] = this[i];
            }

            return result;
        }

        /// <summary>
        /// Returns an array of nested cache arrays. Only different from Cache() for wave types that can contain other waves, like CompositeWave.
        /// </summary>
        public virtual WaveInfo[][] CacheSegmented()
        {
            return new WaveInfo[][] { Cache() };
        }
    }
}
