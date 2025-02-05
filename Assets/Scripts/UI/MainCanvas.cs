using UnityEngine;

namespace Scarcity
{
    public class MainCanvas : MonoBehaviour
    {
        public static Canvas Instance;

        private void Awake()
        {
            Instance = GetComponent<Canvas>();
        }
    }
}