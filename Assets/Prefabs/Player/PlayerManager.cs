using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class PlayerManager : Photon.PunBehaviour
    {

        public static GameObject LocalPlayerInstance;
        public GameObject Laser;

        public GameObject head;

        public List<Color> playerColours;

        private static int colourIndex = -1;

        public static int ColourIndex
        {
            get
            {
                colourIndex++;
                if (colourIndex >= 4)
                {
                    colourIndex = 0;
                }
                return colourIndex;
            }
        }

        private Color playerColour = Color.white;

		// TODO: Use RPCs to add/remove highlights (e.g. specify a position and a colour)
		// TODO: Look at changing colour selection to use RPCs

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
			if (photonView.isMine || !PhotonNetwork.connected)
            {
                // Enable scripts on the camera.
                GetComponentInChildren<Camera>().enabled = true;
                GetComponentInChildren<FlareLayer>().enabled = true;
                GetComponentInChildren<AudioListener>().enabled = true;
                GetComponentInChildren<GvrPointerPhysicsRaycaster>().enabled = true;
                GetComponentInChildren<MovementScript>().enabled = true;

                // Enable scripts on the controller.

                GetComponentInChildren<GvrArmModel>().enabled = true;
                GetComponentInChildren<GvrTrackedController>().enabled = true;

				Laser.SetActive(true);

                photonView.RPC("SetColour", PhotonTargets.AllBufferedViaServer, ColourIndex);
            }
        }

        [PunRPC]
        public void SetColour(int colourIx)
        {
            Debug.Log("My player colour is: " + playerColour);

            // Set the value so buffered RPC calls from existing clients will
            // be accounted for.
            colourIndex = colourIx;

            playerColour = playerColours[colourIx];
            head.GetComponent<Renderer>().material.color = playerColour;
        }
    }
}

