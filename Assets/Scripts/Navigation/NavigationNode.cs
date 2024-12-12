using UnityEngine;

namespace Scarcity
{
    public class NavigationNode : MonoBehaviour
    {
        [SerializeField]
        private NavigationNode[] targetNodes;

        public NavigationNode GetNext()
        {
            return targetNodes.Length != 0 ? targetNodes[Random.Range(0, targetNodes.Length)] : null;
        }
    }
}
