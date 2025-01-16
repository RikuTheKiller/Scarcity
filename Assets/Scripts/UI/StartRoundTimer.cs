using TMPro;
using UnityEngine;

namespace Scarcity
{
    public class StartRoundTimer : MonoBehaviour
    {
        public TextMeshProUGUI counter;

        public float startNextRoundDelay = 30;
        private float startNextRoundTime;

        private void Reset()
        {
            counter = GetComponent<TextMeshProUGUI>();
        }

        private void Awake()
        {
            WaveSpawner.StartNextWaveTimer += OnStartNextWaveTimer;
            WaveSpawner.StartNextWaveEvent += OnStartNextWaveEvent;

            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            WaveSpawner.StartNextWaveTimer -= OnStartNextWaveTimer;
            WaveSpawner.StartNextWaveEvent -= OnStartNextWaveEvent;
        }

        private void Update()
        {
            counter.text = Mathf.CeilToInt(startNextRoundTime - Time.time).ToString();

            if (startNextRoundTime > Time.time) return;

            WaveSpawner.StartNextWave();
        }

        private void OnStartNextWaveTimer()
        {
            gameObject.SetActive(true);
            startNextRoundTime = Time.time + startNextRoundDelay;
        }

        private void OnStartNextWaveEvent()
        {
            gameObject.SetActive(false);
        }
    }
}
