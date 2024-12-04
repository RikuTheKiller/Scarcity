using UnityEngine;

namespace Scarcity
{
    public abstract class Wave : ScriptableObject
    {
        public abstract WaveInfo this[int index] { get; }
        public abstract int Count { get; }

        public WaveInfo[] Cache()
        {
            var result = new WaveInfo[Count];

            for (int i = 0; i < Count; i++)
            {
                result[i] = this[i];
            }

            return result;
        }
    }
}
