using System.Collections;
using UnityEngine;

namespace Scarcity
{
    public class WaveSpawner : MonoBehaviour
    {
        public Wave wave;

        public float startDelay;

        public NavigationNode startNode;

        public Quaternion startRotation;

        private Coroutine waveCoroutine;

        private void Reset()
        {
            startNode = GetComponent<NavigationNode>();
        }

        private void OnEnable()
        {
            waveCoroutine = StartCoroutine(WaveCoroutine());
        }

        private void OnDisable()
        {
            if (waveCoroutine == null) return;
            StopCoroutine(waveCoroutine);
            waveCoroutine = null;
        }

        private IEnumerator WaveCoroutine()
        {
            if (!wave) yield break;

            yield return new WaitForSeconds(startDelay);

            WaveInfo[] waves = wave.Cache();

            foreach (WaveInfo wave in waves)
            {
                GameObject instance = Instantiate(wave.Obj, transform.position, startRotation);

                if (instance && instance.TryGetComponent(out NavigationUser navigationUser))
                {
                    navigationUser.TargetNode = startNode;
                }

                yield return new WaitForSeconds(wave.Delay);
            }
        }
    }
}
