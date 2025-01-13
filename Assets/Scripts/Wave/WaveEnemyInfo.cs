using UnityEngine;

namespace Scarcity
{
    public readonly struct WaveEnemyInfo
    {
        public readonly GameObject Obj;
        public readonly float Delay;

        public WaveEnemyInfo(GameObject obj, float delay)
        {
            Obj = obj;
            Delay = delay;
        }
    }
}