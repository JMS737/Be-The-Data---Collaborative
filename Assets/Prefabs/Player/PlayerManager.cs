using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class PlayerManager : Photon.PunBehaviour
    {
        #region Public Attributes

        public static GameObject LocalPlayerInstance;

        public GameObject laser;

        [Tooltip("This object will have its material colour set to that of the assigned player colour.")]
        public GameObject head;

        public List<Color> playerColours;

        #endregion

        #region Private Attributes

        private PlayerMarker playerMarker;

        // Static variable used to assign different colours to different players.
        private static int colourCounter = -1;

        #endregion

        #region Accessor Variables

        public int NextColourIndex
        {
            get
            {
                colourCounter++;

                if (colourCounter >= playerColours.Count)
                {
                    colourCounter = 0;
                }

                return colourCounter;
            }
        }

        [HideInInspector]
        public int playerColourIndex;

        public Color PlayerColour
        {
            get
            {
                return playerColours[playerColourIndex];
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
			if (photonView.isMine || !PhotonNetwork.connected) {
				Debug.Log ("My Player Spawned");
				PlayerManager.LocalPlayerInstance = this.gameObject;
			} else {
				Debug.Log ("Other Player Spawned");
			}

            DontDestroyOnLoad(this.gameObject);
        }

        // Use this for initialization
        void Start()
        {
            playerMarker = GetComponentInChildren<PlayerMarker>();

            // Enable certain player functionality for the local player only.
			if (photonView.isMine || !PhotonNetwork.connected)
            {
                // Enable scripts on the camera.
                GetComponentInChildren<Camera>().enabled = true;
                GetComponentInChildren<FlareLayer>().enabled = true;
                GetComponentInChildren<AudioListener>().enabled = true;
                GetComponentInChildren<GvrPointerPhysicsRaycaster>().enabled = true;

                // Enable scripts on the controller.
                GetComponentInChildren<GvrArmModel>().enabled = true;
                GetComponentInChildren<GvrTrackedController>().enabled = true;

                laser.SetActive(true);

                if (PhotonNetwork.connected)
                {
                    photonView.RPC("SetColour", PhotonTargets.AllBufferedViaServer, NextColourIndex);
                    PhotonNetwork.player.NickName = "Player " + (colourCounter + 1);
                }
            }
        }

        #endregion

        #region Public Methods

        // Called when the pointed enters/exits any of the players colliders.
        public void Highlight(bool IsHighlighted)
        {
            playerMarker.Highlight(IsHighlighted);
        }

        #endregion

        #region Photon Methods

        // A network call which allows player colours to be synchronised.
        [PunRPC]
        public void SetColour(int colourIx)
        {
            // Update the value so buffered RPC calls from existing clients will
            // be accounted for.
            colourCounter = colourIx;
            playerColourIndex = colourIx;

            head.GetComponent<Renderer>().material.color = PlayerColour;
            GetComponentInChildren<PlayerMarker>().SetColour(PlayerColour);
        }

        #endregion
    }
}

