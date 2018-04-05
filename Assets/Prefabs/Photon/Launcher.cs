using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataVis.Collaboration
{
    public class Launcher : Photon.PunBehaviour
    {
        public byte MaxPlayersPerRoom = 4;
        public PhotonLogLevel LogLevel = PhotonLogLevel.Informational;

        public GameObject controlObj;
        public GameObject connectingObj;
        public Text statusText;

        private string _gameVersion = "1.0";
        private bool isConnecting;

        private void Awake()
        {
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = true;
            PhotonNetwork.logLevel = LogLevel;
        }

        // Use this for initialization
        void Start()
        {
            statusText.text = "Connecting to server...";
            //Connect();
        }

        public void Connect()
        {
            controlObj.SetActive(false);
            connectingObj.SetActive(true);
            isConnecting = true;
            if (PhotonNetwork.connected)
            {
                statusText.text = "Attempting to join room...";
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }

        public override void OnConnectedToMaster()
        {
            statusText.text = "Connected to server...";
            Debug.Log("Launcher: OnConnectedToMaster() called.");

            if (isConnecting)
            {
                statusText.text = "Attempting to join room...";
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnectedFromPhoton()
        {
            Debug.LogWarning("Launcher: OnDiscnnectedFromPhoton() called.");
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            statusText.text = "Creating new room...";
            Debug.Log("Launcher: OnPhotonRandomJoinFailed() was called. No rooms available.\nCreating room...");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        }

        public override void OnJoinedRoom()
        {
            statusText.text = "Room joined successfully...";
            Debug.Log("Launcher: OnJoinedRoom() called.");

            PhotonNetwork.LoadLevel("Main");
        }

        public void OnDatasetButtonPressed(int datasetNumber)
        {
            GameState.ParticipantSet = datasetNumber;
            Connect();
        }
    }
}

