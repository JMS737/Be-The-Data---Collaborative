using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataVis.Collaboration
{
    public class PlayerManager : Photon.PunBehaviour
    {
        public static GameObject LocalPlayerInstance;
        public GameObject laser;

        [Tooltip("This object will have its material colour set to that of the assigned player colour.")]
        public GameObject head;

        public List<Color> playerColours;

        public HUDManager PlayerHUD { get; set; }

        // Static variable used to assign different colours to different players.
        private static int colourCounter = -1;

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

        public int playerColourIndex;

        public Color PlayerColour
        {
            get
            {
                return playerColours[playerColourIndex];
            }
        }

		// TODO: Use RPCs to add/remove highlights (e.g. specify a position and a colour)

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

        public override void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            if (photonView.isMine)
            {
                PhotonNetwork.player.TagObject = this.gameObject;
            }
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

				laser.SetActive(true);

                photonView.RPC("SetColour", PhotonTargets.AllBufferedViaServer, NextColourIndex);

                PhotonNetwork.playerName = "Player " + playerColourIndex;

                PlayerHUD = GetComponent<HUDManager>();

                foreach (PhotonPlayer player in PhotonNetwork.playerList)
                {
                    if (!player.IsLocal)
                    {
                        Debug.Log("Adding player " + playerColourIndex);
                        GameObject playerObj = player.TagObject as GameObject;
                        PlayerManager playerManager = playerObj.GetComponent<PlayerManager>();
                        PlayerHUD.AddPlayerLabel(player.NickName, playerManager.PlayerColour, playerObj.transform);
                        playerManager.PlayerHUD.AddPlayerLabel(PhotonNetwork.player.NickName, PlayerColour, this.transform);
                    }
                }
            }
        }

        //public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        //{
        //    HUDManager playerHUD = GetComponent<HUDManager>();
        //    GameObject playerObj = (GameObject)newPlayer.TagObject;

        //    Debug.Log("HUD = " + playerHUD);
        //    Debug.Log("Obj = " + playerObj);
        //    playerHUD.AddPlayerLabel(newPlayer.NickName, playerObj.GetComponent<PlayerManager>().PlayerColour, playerObj.transform);
        //}


        [PunRPC]
        public void SetColour(int colourIx)
        {
            // Update the value so buffered RPC calls from existing clients will
            // be accounted for.
            colourCounter = colourIx;
            playerColourIndex = colourIx;

            //playerColour = playerColours[colourIx];
            head.GetComponent<Renderer>().material.color = PlayerColour;
        }


    }
}

