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
                var predictedPosition = PredictTarget(target);
                transform.rotation = Quaternion.LookRotation(Vector3.forward, predictedPosition - transform.position);
                gun.pivot.rotation = Quaternion.LookRotation(Vector3.forward, predictedPosition - gun.pivot.position);
                gun.TryFire();
            }
        }

        private Vector3 PredictTarget(Enemy target)
        {
            var distance = Vector3.Distance(target.transform.position, transform.position);
            return target.transform.position + target.navigationUser.speed * (distance / gun.projectilePrefab.speed) * target.transform.up;
        }
    }
}
