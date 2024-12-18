using UnityEngine;

namespace Scarcity
{
    public class NavigationNode : MonoBehaviour
    {
        [SerializeField]
        private NavigationNode[] targetNodes;

        private int targetIndex = 0;

        public NavigationNode GetNext()
        {
            if (targetNodes.Length == 0) return null;
            targetIndex = (targetIndex + 1) % targetNodes.Length;
            return targetNodes[targetIndex];
        }
    }
}
