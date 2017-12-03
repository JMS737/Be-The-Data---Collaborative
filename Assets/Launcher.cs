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

        public Text statusText;

        private string _gameVersion = "1.0";

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
            Connect();
        }

        public void Connect()
        {
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
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnectedFromPhoton()
        {
            Debug.LogWarning("Launcher: OnDiscnnectedFromPhoton() called.");
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            statusText.text = "No room found. Creating new room...";
            Debug.Log("Launcher: OnPhotonRandomJoinFailed() was called. No rooms available.\nCreating room...");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        }

        public override void OnJoinedRoom()
        {
            statusText.text = "Room joined successfully...";
            Debug.Log("Launcher: OnJoinedRoom() called.");
        }
    }
}

