using System;
using UnityEngine;

namespace Scarcity
{
    public class Money
    {
        private static float money = 120;

        /// <summary>
        /// Sent when the amount of money changes. (money, oldMoney)
        /// </summary>
        public static Action<float, float> Changed;

        public static float Get() => money;

        public static void Set(float value)
        {
            var oldMoney = money;
            money = Mathf.Max(0, value);

            if (oldMoney != money)
            {
                Changed?.Invoke(money, oldMoney);
            }
        }

        public static void Adjust(float value) => Set(money + value);

        public static bool Buy(float cost)
        {
            if (cost > money) return false;
            Adjust(-cost);
            return true;
        }
    }
}
