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

        public float maxSpeed = 4;
        public float accelerationTime = 0.2f;
        public float turnSpeed = 2;

        public Texture2D cursorTexture;
        public Vector2 cursorOffset;

        private void Reset()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.Auto);
        }

        private void OnDisable()
        {
            Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
        }

        private void FixedUpdate()
        {
            rigidbody.linearVelocity = Vector3.MoveTowards(rigidbody.linearVelocity, Input.Move.Value * maxSpeed, Time.deltaTime * maxSpeed / accelerationTime);
        }

        private void Update()
        {
            Quaternion target = Quaternion.LookRotation(Vector3.forward, (Vector2)Camera.main.ScreenToWorldPoint(Input.Point.Value) - (Vector2)transform.position);
            pivot.rotation = Quaternion.RotateTowards(pivot.rotation, target, Time.deltaTime * turnSpeed * 360);
        }
    }
}
