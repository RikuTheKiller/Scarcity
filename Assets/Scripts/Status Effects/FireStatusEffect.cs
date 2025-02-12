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

        private ParticleSystem particles;

        private void OnEnable()
        {
            particles = Instantiate(ParticleEffectReferences.Fire, transform);

            ResetTime();
            StartCoroutine(DamageRoutine());
        }

        private void OnDisable()
        {
            Destroy(particles.gameObject);
            particles = null;

            StopAllCoroutines();
            Destroy(this);
        }

        private void Update()
        {
            particles.transform.rotation = Quaternion.identity; // Thought you had seen shitcode yet? Think again.
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