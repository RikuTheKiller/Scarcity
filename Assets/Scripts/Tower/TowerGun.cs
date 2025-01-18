using UnityEngine;

namespace Scarcity
{
    public class TowerGun : MonoBehaviour
    {
        public Gun gun;
        public float range = 5;

        private void Update()
        {
            Enemy target = null;
            float closest = range;

            foreach (var enemy in Enemy.All)
            {
                var distance = Vector3.SqrMagnitude(enemy.transform.position - transform.position);

                if (distance < closest)
                {
                    target = enemy;
                    closest = distance;
                }
            }

            if (target)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
                gun.pivot.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - gun.pivot.position);
                gun.TryFire();
            }
        }
    }
}
