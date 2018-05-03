using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataVis.Collaboration
{
    public enum Platform
    {
        GoogleDaydream,
        GoogleCardboard
    }

    public class GameManager : Photon.PunBehaviour
    {
        
        //public GameObject playerPrefab;
        public Platform platform;

        private string playerPrefabName;

        [Tooltip("The graph object the player should spawn by.")]
        public GraphManager graphManager;

        private void Start()
        {
            playerPrefabName = "";

            switch (platform){
                case Platform.GoogleCardboard:
                    playerPrefabName = "PlayerVR_Cardboard";
                    break;

                case Platform.GoogleDaydream:
                    playerPrefabName = "PlayerVR_Daydream";
                    break;
            }
            StartCoroutine("WaitAndSpawn");
        }

		// Wait a small amount of time for the graph to be populated so the player can be
		// spawned in the centre of the data.
        IEnumerator WaitAndSpawn()
        {
            yield return new WaitForSeconds(0.05f);
            if (playerPrefabName == "")
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference.");
            }
            else
            {
				Vector3 spawnPoint = graphManager.GetSpawnPoint();
				if (PlayerManager.LocalPlayerInstance == null && PhotonNetwork.connected)
                {
                    Debug.Log("Instantiating Local Player...");
                    
                    PhotonNetwork.Instantiate(playerPrefabName, spawnPoint, Quaternion.identity, 0);
                }
                else
                {
                    Debug.Log("Ignoring scene load.");
                    Instantiate (Resources.Load(playerPrefabName), spawnPoint, Quaternion.identity);
                }
            }
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void Quit()
        {
            PhotonNetwork.Disconnect();
            Application.Quit();
        }

        #region Photon Callbacks

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            Debug.Log("GameManager: Player connected.");

            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("Player is master client.");

                //LoadScene();
            }
        }

        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            Debug.Log("GameManager: Player disconnected.");

            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("Player is master client.");

                //LoadScene();
            }
        }

        #endregion
    }
}

