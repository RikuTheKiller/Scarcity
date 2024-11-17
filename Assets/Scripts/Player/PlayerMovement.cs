using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scarcity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody2D rigidbody;
        public Transform pivot;

        public float maxSpeed = 3;
        public float accelerationTime = 0.2f;

        private Coroutine moveRoutine;

        private void Reset()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, Input.Move.Value * maxSpeed, Time.deltaTime * maxSpeed / accelerationTime);
        }

        private void OnEnable()
        {
            Input.PointerPosition.Performed += OnPointerPosition;
        }

        private void OnDisable()
        {
            Input.PointerPosition.Performed -= OnPointerPosition;
        }

        private void OnPointerPosition(Vector2 position)
        {
            pivot.up = (Vector2)Camera.main.ScreenToWorldPoint(position) - (Vector2)transform.position;
        }
    }
}
