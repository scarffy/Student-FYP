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

        [Header("Multiplayer connection state")]
        public bool readyToConnect = false;
        public bool isInRoom = false;

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

            //! To delete this once done
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Multiplayer_Environment", UnityEngine.SceneManagement.LoadSceneMode.Additive);
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
            readyToConnect = true;

            if(Data.UserLocalSaveFile.Instance != null)
            {
                Data.UserLocalSaveFile.Instance.LoadData();
                Debug.Log(Data.UserLocalSaveFile.Instance.saveDataString);
                ExitGames.Client.Photon.Hashtable userHash = new ExitGames.Client.Photon.Hashtable();

                // userHash.Add("Key", "Value");
                userHash.Add("UserInfos", Data.UserLocalSaveFile.Instance.saveDataString);
                userHash.Add("PlayFabID", Data.UserLocalSaveFile.Instance.saveData.playfabId);

                PhotonNetwork.LocalPlayer.SetCustomProperties(userHash);
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            readyToConnect = false;
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            isInRoom = false;
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
            isInRoom = true;
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            PhotonController.Instance.RemoveOtherPlayer();
        }

        #endregion

        #region Other Player Event
        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            PhotonController.Instance.RemoveOtherPlayer(otherPlayer);
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
            if(photonEvent.Code == 0)
            {
                object[] data = (object[])photonEvent.CustomData;

                GameObject player = Instantiate(PhotonController.Instance.playerPrefab, (Vector3)data[0], (Quaternion)data[1]);
                player.transform.SetParent(PhotonController.Instance.playersParent.transform);
                //! Find a way to set gameobject name

                //player.GetComponent<PhotonPlayerController>().InMultiplayerOther();
                player.GetComponent<PhotonPlayerController>().InMultiplayerOther(PhotonNetwork.CurrentRoom.GetPlayer(photonEvent.Sender));
                PhotonView photonView = player.GetComponent<PhotonView>();
                photonView.ViewID = (int)data[2];
            }

            //if (photonEvent.Sender != -1)
            //{
            //    switch (photonEvent.Code)
            //    {
            //        // event to manual instantiate
            //        case CustomManualInstantiationEventCode.VIEW_ID:
            //            Photon.Realtime.Player player = PhotonNetwork.CurrentRoom.GetPlayer(photonEvent.Sender);
            //            object[] data = (object[])photonEvent.CustomData;
            //            PhotonController.Instance.InstantiatePlayer(player, (int)data[2]);
            //            break;
            //    }
            //}
        }
    }
}