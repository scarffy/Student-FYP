using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using ExitGames.Client.Photon;

namespace FYP.Backend {
    public class PhotonManager : MonoBehaviourPunCallbacks,IOnEventCallback
    {
        public static PhotonManager Instance;

        [HideInInspector]
        public int roomIndex = 0;
        public string[] roomName;

        [SerializeField]
        private byte maxPlayersPerRoom = 20;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1,UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }

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
            Debug.Log("Photon connected and ready to roll");
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

        public void CreateRoomOrJoin()
        {
            base.OnCreatedRoom();

            RoomOptions roomOptions = new RoomOptions()
            {
                IsOpen = true,
                IsVisible = true,
                MaxPlayers = maxPlayersPerRoom,
                CleanupCacheOnLeave = true
            };
            PhotonNetwork.JoinOrCreateRoom(roomName[roomIndex], roomOptions, TypedLobby.Default);
        }

        #endregion

        #region Join Room

        public Action OnJoin;
        public override void OnJoinedRoom()
        {
            //! This is the place where you spawn a player
            base.OnJoinedRoom();
            OnJoin?.Invoke();
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

        public void SendViewId(PhotonView photonView)
        {
            photonView.ViewID = PhotonNetwork.AllocateViewID(PhotonNetwork.LocalPlayer.ActorNumber);
            //object[] data = { photonView.ViewID };
            object[] data = new object[]
            {
                photonView.gameObject.transform.position, photonView.gameObject.transform.rotation, photonView.ViewID
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                CachingOption = EventCaching.AddToRoomCache
            };

            PhotonNetwork.RaiseEvent(CustomManualInstantiationEventCode.VIEW_ID, data, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Sender != -1)
            {
                switch (photonEvent.Code)
                {
                    // event to manual instantiate
                    case CustomManualInstantiationEventCode.VIEW_ID:
                        //_ = PhotonNetwork.CurrentRoom.GetPlayer(photonEvent.Sender);
                        //object[] data = (object[])photonEvent.CustomData;
                        break;
                }
            }
        }
    }
}