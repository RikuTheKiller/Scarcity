using System;
using UnityEngine;

namespace Scarcity
{
    public class NavigationNode : MonoBehaviour
    {
        [SerializeField]
        private NavigationNode[] targetNodes;

        private int targetIndex = 0;

        public event Action<GameObject> Arrived;

        public NavigationNode GetNext()
        {
            if (targetNodes.Length == 0) return null;
            targetIndex = (targetIndex + 1) % targetNodes.Length;
            return targetNodes[targetIndex];
        }

        public void InvokeArrived(GameObject target)
        {
            Arrived?.Invoke(target);
        }
    }
}
