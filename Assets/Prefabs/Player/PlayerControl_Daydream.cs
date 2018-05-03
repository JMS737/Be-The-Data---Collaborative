using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerControl_Daydream : Photon.PunBehaviour
    {
        #region Public Attributes

        public float swipeStrength = 0.5f;

        #endregion

        #region Private Attributes

        // Input variables
        private Vector2 firstTouch, secondTouch, deltaTouch;
        private float firstTime, secondTime, deltaTime;

        private Transform cameraTransform;

        private PlayerMovement playerMovement;

        #endregion

        #region Unity Methods

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
            if (photonView.isMine || !PhotonNetwork.connected)
            {
                HandleInput();
            }
        }

        #endregion

        #region Private Methods

        // Detects when the user touches and releases the touchpad on the GVR Controller
        // and uses this to determine when the user performs a swipe.
        private void HandleInput()
        {
            if (GvrControllerInput.TouchDown)
            {
                firstTouch = GvrControllerInput.TouchPos;
                firstTime = Time.time;
            }

            if (GvrControllerInput.TouchUp)
            {
                secondTouch = GvrControllerInput.TouchPos;
                secondTime = Time.time;

                deltaTouch = secondTouch - firstTouch;
                deltaTime = secondTime - firstTime;

                Vector3 newPosition = CalculateNewPosition(deltaTouch, deltaTime);

                playerMovement.MoveTo(newPosition);
            }
        }

        // Given an input on the GVR Controller (swipe distance, direction and time), this method
        // translates that into a new position for the player.
        private Vector3 CalculateNewPosition(Vector2 deltaTouch, float deltaTime)
        {
            Vector3 deltaMove;

            // Extract the dominant direction.
            if (Mathf.Abs(deltaTouch.x) > Mathf.Abs(deltaTouch.y))
            {
                deltaMove = new Vector3(deltaTouch.x, 0.0f, 0.0f);
            }
            else
            {
                deltaMove = new Vector3(0.0f, 0.0f, deltaTouch.y);
            }

            deltaMove = deltaMove * swipeStrength / deltaTime;

            // Generate forward/backward and right/left components of the move relative to
            // the player's orientation.
            Vector3 forwardComponent = cameraTransform.forward * -deltaMove.z;
            Vector3 rightComponent = cameraTransform.right * deltaMove.x;

            deltaMove = forwardComponent + rightComponent;

            return transform.position + deltaMove;
        }

        #endregion
    }
}

