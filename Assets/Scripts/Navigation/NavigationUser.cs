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
            var targetRotation = Quaternion.LookRotation(Vector3.forward, TargetNode.transform.position - transform.position);
            var finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * 360 * Time.deltaTime);
            var finalPosition = transform.position;

            var step = speed * Time.deltaTime;

            while (TargetNode && step > 0)
            {
                var distance = Vector3.Distance(transform.position, TargetNode.transform.position);

                finalPosition = Vector3.MoveTowards(finalPosition, TargetNode.transform.position, step);

                step -= distance;

                if (finalPosition == TargetNode.transform.position)
                {
                    TargetNode = TargetNode.GetNext();
                }
            }

            transform.SetPositionAndRotation(finalPosition, finalRotation);
        }
    }
}
