using UnityEngine;

namespace Scarcity
{
    public class WaveInfo
    {
        public readonly GameObject Obj;
        public readonly float Delay;

        public WaveInfo(GameObject obj, float delay)
        {
            Obj = obj;
            Delay = delay;
        }
    }
}