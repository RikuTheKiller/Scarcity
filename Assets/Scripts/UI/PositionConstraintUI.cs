using UnityEngine;

namespace Scarcity
{
    [RequireComponent(typeof(RectTransform))]
    public class PositionConstraintUI : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

        private Camera camera;
        private RectTransform rectTransform;

        private void Start()
        {
            camera = Camera.main;
            rectTransform = (RectTransform)transform;

            UpdatePosition();
        }

        private void Update() => UpdatePosition();

        private void UpdatePosition()
        {
            if (!target) return;
            rectTransform.position = camera.WorldToScreenPoint(target.position + offset);
        }
    }
}