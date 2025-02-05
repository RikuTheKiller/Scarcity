using System.Collections;
using UnityEngine;

namespace Scarcity
{
    public class FireStatusEffect : MonoBehaviour
    {
        private float interval = 0.2f;
        private int damage = 1;
        private float endTime = 0;
        private float duration = 8;

        private void OnEnable()
        {
            ResetTime();
            StartCoroutine(DamageRoutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            Destroy(this);
        }

        public void ResetTime()
        {
            endTime = Time.time + duration;
        }

        private IEnumerator DamageRoutine()
        {
            while (endTime > Time.time)
            {
                yield return new WaitForSeconds(interval);

                var health = GetComponent<Health>();
                if (health)
                {
                    health.TakeDamage(damage);
                }
            }

            Destroy(this);
        }
    }
}