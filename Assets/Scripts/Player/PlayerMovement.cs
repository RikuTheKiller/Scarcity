using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scarcity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody2D rigidbody;

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
    }
}
