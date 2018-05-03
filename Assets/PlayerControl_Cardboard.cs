using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerControl_Cardboard : Photon.PunBehaviour
    {
        [Range(0.1f, 10f)]
        public float MovementStep = 5.0f;

        [Range(0f, 1f)]
        public float doubleClickTime = 0.25f;

        private bool oneClick = false;
        private float firstClickTime;

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

        // Detects when the user touches and releases the touchpad on the GVR Controller
        // and uses this to determine when the user performs a swipe.
        private void HandleInput()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (oneClick && (Time.time - firstClickTime) <= doubleClickTime)
                {
                    // Double click
                    Debug.Log("Double click");
                    oneClick = false;

                    Vector3 newPosition = MoveForward();

                    playerMovement.MoveTo(newPosition);
                }
                else
                {
                    // Set first click.
                    oneClick = true;
                    firstClickTime = Time.time;
                }
            }

            if (oneClick && (Time.time - firstClickTime) > doubleClickTime)
            {
                // Single Click
                Debug.Log("Single click");
                oneClick = false;
            }
        }

        // Given an input on the GVR Controller (swipe distance, direction and time), this method
        // translates that into a new position for the player.
        private Vector3 MoveForward()
        {
            Vector3 forward = cameraTransform.forward;

            return transform.position + (forward * MovementStep);
        }
    }
}

