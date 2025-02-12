using TMPro;
using UnityEngine;

namespace Scarcity
{
    public class EnemyCounter : MonoBehaviour
    {
        private TextMeshProUGUI textMeshPro;

        private void Awake()
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();

            UpdateCounter(0);
            Enemy.EnemyCountChanged += UpdateCounter;
        }

        private void OnDestroy()
        {
            Enemy.EnemyCountChanged -= UpdateCounter;
        }

        private void UpdateCounter(int count)
        {
            gameObject.SetActive(count > 0);

            if (gameObject.activeSelf)
            {
                textMeshPro.text = $"Enemies remaining: {count}";
            }
        }
    }
}
