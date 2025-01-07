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

        private void OnEnable()
        {
            button.onClick.AddListener(WaveSpawner.StartNextWave);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(WaveSpawner.StartNextWave);
        }
    }
}
