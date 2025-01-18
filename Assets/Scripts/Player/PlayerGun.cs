using UnityEngine;

namespace Scarcity
{
    public class PlayerGun : MonoBehaviour
    {
        public Gun gun;

        private void Reset()
        {
            gun = GetComponent<Gun>();
        }

        private void Update()
        {
            if (Input.Attack)
            {
                gun.TryFire();
            }
        }
    }
}