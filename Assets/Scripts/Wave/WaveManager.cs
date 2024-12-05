using System.Collections;
using UnityEngine;

namespace Scarcity
{
    public class WaveManager : MonoBehaviour
    {
        public Wave wave;

        private Coroutine waveCoroutine;

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

            WaveInfo[] waves = wave.Cache();

            foreach (WaveInfo wave in waves)
            {
                Instantiate(wave.Obj, transform.position, transform.rotation);
                yield return new WaitForSeconds(wave.Delay);
            }
        }
    }
}
