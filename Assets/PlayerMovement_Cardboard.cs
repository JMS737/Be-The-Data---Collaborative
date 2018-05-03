using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class PlayerMovement_Cardboard : MonoBehaviour
    {

        [Range(0.1f, 10f)]
        public float MovementStep = 5.0f;

        [Range(1, 10)]
        public float moveSpeed = 3.0f;

        private Vector3 newPosition;

        private void Start()
        {
            newPosition = transform.position;
        }

        public void MoveForward()
        {
            Vector3 forward = GetComponentInChildren<Camera>().transform.forward;
            newPosition = transform.position + (forward * MovementStep);
        }

        private void Update()
        {
            Vector3 distanceTo = newPosition - transform.position;

            // Divide by the magnitude to normalise the speed throughout the move.
            Vector3 change = distanceTo * Time.deltaTime * moveSpeed / distanceTo.magnitude;

            if (newPosition != transform.position)
            {
                transform.position += change;
                if (distanceTo.magnitude < 0.1f)
                {
                    newPosition = transform.position;
                }
            }
        }

        public void SetPosition(Vector3 position)
        {
            newPosition = position;
        }
    }
}

