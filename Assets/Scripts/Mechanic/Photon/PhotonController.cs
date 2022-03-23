using UnityEngine;
using Photon.Pun;

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
                else
                {
                    players[i].OnLeftRoom();
                }
            }
        }

        /// <summary>
        /// Other player leave room
        /// </summary>
        /// <param name="player"></param>
        public void RemoveOtherPlayer(Photon.Realtime.Player player)
        {
            string pName = (string)player.CustomProperties["PlayFabID"];
            Transform transform = playersParent.transform.Find(pName);
            if (transform)
                Destroy(transform.gameObject);
        }
    }
}