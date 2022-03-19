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
    }
}