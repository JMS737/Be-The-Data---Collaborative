using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerControl_Keyboard : Photon.PunBehaviour
    {
        public float MoveDistance = 0.1f;

        private Transform cameraTransform;

        private PlayerMovement playerMovement;

        // Use this for initialization
        void Start()
        {
            cameraTransform = GetComponentInChildren<Camera>().transform;
            playerMovement = GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            // Only handle input for the local player.
            if (photonView.isMine)
            {
                HandleInput();
            }
        }

        private void HandleInput()
        {
            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            Vector3 moveVector = new Vector3(hAxis, 0.0f, -vAxis);

            if (moveVector != Vector3.zero)
            {
                Vector3 newPosition = CalculateNewPosition(moveVector);
                playerMovement.MoveTo(newPosition);
            }
        }

        private Vector3 CalculateNewPosition(Vector3 deltaMove)
        {
            deltaMove = deltaMove * MoveDistance / Time.deltaTime;

            // Generate forward/backward and right/left components of the move relative to
            // the player's orientation.
            Vector3 forwardComponent = cameraTransform.forward * -deltaMove.z;
            Vector3 rightComponent = cameraTransform.right * deltaMove.x;

            deltaMove = forwardComponent + rightComponent;

            return transform.position + deltaMove;
        }
    }
}

