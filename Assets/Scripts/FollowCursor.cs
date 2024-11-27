using UnityEngine;

namespace Scarcity
{
    public class FollowCursor : MonoBehaviour
    {
        public Transform pivot;

        public bool instant = false;
        public float turnSpeed = 2;

        private void Reset()
        {
            pivot = transform;
        }

        private void Update()
        {
            Quaternion target = Quaternion.LookRotation(Vector3.forward, (Vector2)Camera.main.ScreenToWorldPoint(Input.Point.Value) - (Vector2)transform.position);

            if (instant)
            {
                pivot.rotation = target;
            }
            else
            {
                pivot.rotation = Quaternion.RotateTowards(pivot.rotation, target, Time.deltaTime * turnSpeed * 360);
            }
        }
    }
}
