using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scarcity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody2D rigidbody;
        public Transform pivot;

        public float maxSpeed = 4;
        public float accelerationTime = 0.2f;

        private void Reset()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rigidbody.linearVelocity = Vector3.MoveTowards(rigidbody.linearVelocity, Input.Move.Value * maxSpeed, Time.deltaTime * maxSpeed / accelerationTime);
        }

        private void OnEnable()
        {
            Input.Point.Performed += OnPointerPosition;
        }

        private void OnDisable()
        {
            Input.Point.Performed -= OnPointerPosition;
        }

        private void OnPointerPosition(Vector2 position)
        {
            pivot.rotation = Quaternion.LookRotation(Vector3.forward, (Vector2)Camera.main.ScreenToWorldPoint(position) - (Vector2)transform.position);
        }
    }
}
