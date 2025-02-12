using UnityEngine;

namespace Scarcity
{
    public class ParticleEffectReferences : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem fire;
        public static ParticleSystem Fire;

        private void Awake()
        {
            Fire = fire;
        }
    }
}
