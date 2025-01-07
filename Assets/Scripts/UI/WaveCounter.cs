using UnityEngine;
using TMPro;

namespace Scarcity
{
    public class WaveCounter : MonoBehaviour
    {
        public TextMeshProUGUI display;

        private void OnEnable()
        {
            WaveSpawner.StartNextWaveEvent += OnStartNextWave;
        }

        private void OnDisable()
        {
            WaveSpawner.StartNextWaveEvent -= OnStartNextWave;
        }

        private void OnStartNextWave()
        {
            display.text = $"Wave {WaveSpawner.waveIndex}";
        }
    }
}
