using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

namespace FYP.Backend
{
    public class PhotonController : MonoBehaviourPunCallbacks
    {
        public static PhotonController Instance;

        public GameObject playerPrefab;

        public int roomIndex;

        void Start()
        {
            Instance = this;
        }

        public void JoinOrCreateRoom()
        {
            PhotonManager.Instance.roomIndex = roomIndex;
            PhotonManager.Instance.CreateRoomOrJoin();
        }

        public void LeaveRoom()
        {
            PhotonManager.Instance.LeaveRoom();
        }

        public void InstantiatePlayer(Photon.Realtime.Player player, int photonViewId)
        {
            GameObject playerGo = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
            //playerGo.GetComponent<PhotonPlayerController>().InMultiplayerOther();
        }
    }
}