using UnityEngine;

namespace Scarcity
{
    public class NavigationUser : MonoBehaviour
    {
        public float speed = 2;
        public float turnSpeed = 1;

        public NavigationNode TargetNode
        {
            get => m_TargetNode;

            set
            {
                enabled = value;
                m_TargetNode = value;
            }
        }

        [SerializeField]
        private NavigationNode m_TargetNode;

        private void Awake()
        {
            enabled = TargetNode;
        }

        private void Update()
        {
            var targetRotation = Quaternion.LookRotation(Vector3.forward, TargetNode.transform.position);
            var finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180 * Time.deltaTime);
            var finalPosition = Vector3.MoveTowards(transform.position, TargetNode.transform.position, turnSpeed * 360 * Time.deltaTime);

            transform.SetPositionAndRotation(finalPosition, finalRotation);
        }
    }
}
