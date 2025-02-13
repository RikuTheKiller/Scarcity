using UnityEngine;

namespace Scarcity
{
    public class StuffRefs : MonoBehaviour
    {
        public static StuffRefs Instance;

        public GameObject GameOver;

        private void Awake()
        {
            Instance = this;
        }
    }
}