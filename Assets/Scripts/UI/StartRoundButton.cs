using UnityEngine;
using UnityEngine.UI;

namespace Scarcity
{
    public class StartRoundButton : MonoBehaviour
    {
        public Button button;

        private void Reset()
        {
            button = GetComponent<Button>();
        }

        private void Awake()
        {
            WaveSpawner.StartNextWaveTimer += OnStartNextWaveTimer;
            WaveSpawner.StartNextWaveEvent += OnStartNextWaveEvent;
        }

        private void OnDestroy()
        {
            WaveSpawner.StartNextWaveTimer -= OnStartNextWaveTimer;
            WaveSpawner.StartNextWaveEvent -= OnStartNextWaveEvent;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(WaveSpawner.StartNextWave);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(WaveSpawner.StartNextWave);
        }

        private void OnStartNextWaveTimer()
        {
            gameObject.SetActive(true);
        }

        private void OnStartNextWaveEvent()
        {
            gameObject.SetActive(false);
        }
    }
}
