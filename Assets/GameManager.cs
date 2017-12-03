using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataVis.Collaboration
{
    public class GameManager : Photon.PunBehaviour
    {
        public GameObject playerPrefab;

        private void Start()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference.");
            }
            else
            {
                
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.Log("Instantiating Local Player...");
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0.0f, 1.6f, 0.0f), Quaternion.identity, 0);
                }
                else
                {
                    Debug.Log("Ignoring scene load.");
                }
            }
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
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
    }
}

