using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using ExitGames.Client.Photon;

namespace FYP.Backend
{
    public class PhotonManager : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        public static PhotonManager Instance;

        public Action OnJoin;

        [HideInInspector]
        public int roomIndex = 0;
        public string[] roomName;

        [SerializeField]
        private byte maxPlayersPerRoom = 20;

        [Header("Multiplayer connection state")]
        public bool readyToConnect = false;
        public bool isInRoom = false;

        [Header("Player")]
        [SerializeField] Transform playerArrayParent;
        [SerializeField] List<Transform> players;
        public bool isMasterClient;

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

            if (Data.UserLocalSaveFile.Instance != null)
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
        public override void OnJoinedRoom()
        {
            //! This is the place where you spawn a player
            base.OnJoinedRoom();
            OnJoin?.Invoke();
            isInRoom = true;

            //! This is to get the master client but can already get from PhotonNetwork.MasterClient
            UpdatePlayerList();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            PhotonController.Instance.RemoveOtherPlayer();
        }

        #endregion

        #region Other Player Event
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            UpdatePlayerList();
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            PhotonController.Instance.RemoveOtherPlayer(otherPlayer);
            UpdatePlayerList();
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

        #region PlayerEvents

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
            if (photonEvent.Code == 0)
            {
                object[] data = (object[])photonEvent.CustomData;

                GameObject player = Instantiate(PhotonController.Instance.playerPrefab, (Vector3)data[0], (Quaternion)data[1]);
                player.transform.SetParent(PhotonController.Instance.playersParent.transform);

                //player.GetComponent<PhotonPlayerController>().InMultiplayerOther();
                player.GetComponent<PhotonPlayerController>().InMultiplayerOther(PhotonNetwork.CurrentRoom.GetPlayer(photonEvent.Sender));
                PhotonView photonView = player.GetComponent<PhotonView>();
                photonView.ViewID = (int)data[2];
            }
        }

        //! No use for this yet.
        //! Delete if not used.
        public void UpdatePlayerList()
        {
            players = new List<Transform>();

            foreach (Transform item in playerArrayParent)
            {
                players.Add(item);
            }

            if (PhotonNetwork.IsMasterClient)
                isMasterClient = true;
            else
                isMasterClient = false;
        }

        #endregion
    }
}