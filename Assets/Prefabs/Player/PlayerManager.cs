using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class PlayerManager : Photon.PunBehaviour
    {

        public static GameObject LocalPlayerInstance;

        public GameObject Laser;

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

//				playerColour = playerColours [ColourIndex];
            }
        }
    }
}

