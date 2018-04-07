using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    /**
     * Moves the transform of the attached GameObject towards the defined position
     * each frame.
     */
    public class PlayerMovement : Photon.PunBehaviour {

        #region Public Attributes

        [Range(1, 10)]
        public float moveSpeed = 7.5f;

        #endregion

        #region Private Attributes

        private Vector3 nextPosition;

        private float snapThreshold = 0.1f;

        // Used to determine if the current transit can be interupted by another move.
        private bool inLockedTransit = false;

        #endregion

        #region Unity Methods

        // Use this for initialization
        void Start () {
            // Prevents player moving to origin upon scene load.
            nextPosition = transform.position;
        }
	
	    // Update is called once per frame
	    void Update () {
            // Only handle movement for the local player.
            // Note: Remote players are moved using the PhotonTransformView script.
            if (photonView.isMine)
            {
                HandleMovementForFrame();
            }
        }

        #endregion

        #region Public Methods

        // Sets a new position for the player to move to and states whether the
        // move can be interrupted by another move command.
        public void MoveTo(Vector3 position, bool isTransitInteruptable = true)
        {
            if (!inLockedTransit)
            {
                nextPosition = position;
                inLockedTransit = !isTransitInteruptable;
            }
        }

        #endregion

        #region Private Methods

        // Moves the player towards 'nextPosition' each frame.
        private void HandleMovementForFrame()
        {
            if (nextPosition != transform.position)
            {
                Vector3 distanceTo = nextPosition - transform.position;

                // If the transform is close enough to the next position, "snap" it
                // into position.
                // Note: This stops the player jittering around the desired position.
                if (distanceTo.magnitude < snapThreshold)
                {
                    transform.position = nextPosition;
                    inLockedTransit = false;
                }
                // Else calculate a movement vector for this frame.
                else
                {
                    // Divide by the distance to ensure linear speed throughout transit.
                    Vector3 changeThisFrame = distanceTo * Time.deltaTime * moveSpeed / distanceTo.magnitude;

                    transform.position += changeThisFrame;
                }
            }
        }

        #endregion
    }
}
