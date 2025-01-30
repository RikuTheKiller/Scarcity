using TMPro;
using UnityEngine;

namespace Scarcity
{
    public class MoneyDisplay : MonoBehaviour
    {
        public TextMeshProUGUI TMP;

        private void Reset()
        {
            TMP = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            UpdateDisplay(Money.Get(), 0);
            Money.Changed += UpdateDisplay;
        }

        private void OnDisable()
        {
            Money.Changed -= UpdateDisplay;
        }

        private void UpdateDisplay(float money, float oldMoney)
        {
            TMP.text = "$" + money;
        }
    }
}
