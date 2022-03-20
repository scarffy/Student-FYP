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
        public GameObject playersParent;

        void Start()
        {
            Instance = this;
        }

        public void JoinOrCreateRoom(int value)
        {
            //! Add details to sync such as playfab id

            PhotonManager.Instance.roomIndex = value;
            PhotonManager.Instance.CreateRoomOrJoin();
        }

        public void LeaveRoom()
        {
            PhotonManager.Instance.LeaveRoom();
        }

        /// <summary>
        /// current Player leave room
        /// </summary>
        public void RemoveOtherPlayer()
        {
            PhotonPlayerController[] players = playersParent.transform.GetComponentsInChildren<PhotonPlayerController>();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].isOtherPlayer)
                {
                    Destroy(players[i].gameObject);
                }
            }
        }

        /// <summary>
        /// Other player leave room
        /// </summary>
        /// <param name="player"></param>
        public void RemoveOtherPlayer(Photon.Realtime.Player player)
        {
            // string pName = (string)player.CustomProperties["PlayFabID"];
            
        }
    }
}