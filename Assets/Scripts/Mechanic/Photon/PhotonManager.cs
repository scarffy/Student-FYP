using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

namespace FYP.Backend {
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        public int roomIndex = 0;
        public string[] roomName;

        [SerializeField]
        private byte maxPlayersPerRoom = 20;

        void Start()
        {
            Connect();
        }

        void Connect()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #region Create Room

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            RoomOptions roomOptions = new RoomOptions()
            {
                IsOpen = true,
                IsVisible = true,
                MaxPlayers = maxPlayersPerRoom,
                CleanupCacheOnLeave = true
            };
            PhotonNetwork.JoinOrCreateRoom(roomName[roomIndex], roomOptions,TypedLobby.Default);
        }

        #endregion

        #region Join Room
        public override void OnJoinedRoom()
        {
            //! This is the place where you spawn a player
            base.OnJoinedRoom();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
        }

        #endregion

        #region Fail Handling

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
        }
        #endregion

        #region Properties Update
        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            base.OnRoomPropertiesUpdate(propertiesThatChanged);
        }

        public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        }

        #endregion
    }
}