using System;
using System.Collections;
using UnityEngine;

namespace Scarcity
{
    public class WaveSpawner : MonoBehaviour
    {
        public Wave wave;

        public NavigationNode startNode;

        public Quaternion startRotation;

        private WaveCacheArray cachedWaves;

        public static int waveIndex;
        public static int maxWaveIndex;
        public static int activeSpawnerCount;

        public static event Action StartNextWaveEvent;
        public static event Action StartNextWaveTimer;

        private void Reset()
        {
            startNode = GetComponent<NavigationNode>();
        }

        private void Awake()
        {
            cachedWaves = wave.ToCacheArray();
            maxWaveIndex = Math.Max(maxWaveIndex, cachedWaves.Length - 1);
        }

        private void OnEnable()
        {
            StartNextWaveEvent += StartNextWaveInternal;
        }

        private void OnDisable()
        {
            StartNextWaveEvent -= StartNextWaveInternal;
            StopAllCoroutines();
        }

        public static void StartNextWave()
        {
            activeSpawnerCount = 0;

            if (waveIndex > maxWaveIndex) return;
            StartNextWaveEvent?.Invoke();

            if (waveIndex >= maxWaveIndex) return;
            waveIndex++;
        }

        private void StartNextWaveInternal()
        {
            if (waveIndex > cachedWaves.Length) return;

            var wave = cachedWaves[waveIndex];
            if (wave.Length == 0) return;

            StartCoroutine(WaveCoroutine(wave));
        }

        private IEnumerator WaveCoroutine(WaveCache wave)
        {
            activeSpawnerCount++;

            for (int i = 0; i < wave.Length; i++)
            {
                WaveEnemyInfo enemyInfo = wave[i];

                GameObject instance = Instantiate(enemyInfo.Obj, transform.position, startRotation);

                if (instance && instance.TryGetComponent(out NavigationUser navigationUser))
                {
                    navigationUser.TargetNode = startNode;
                }

                if (i != wave.Length - 1)
                {
                    yield return new WaitForSeconds(enemyInfo.Delay);
                }
            }

            activeSpawnerCount--;

            if (activeSpawnerCount == 0 && !wave.Equals(cachedWaves[^1]))
            {
                StartNextWaveTimer?.Invoke();
            }
        }
    }
}
