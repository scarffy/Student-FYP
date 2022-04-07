using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.Backend
{
    public class PhotonAreaTrigger : MonoBehaviour
    {
        public PhotonPlayerController plController;

        /// <summary>
        /// To be used with PhotonManager and PhotonController
        /// </summary>
        [SerializeField]
        int roomIndex = 0;
        [SerializeField]
        string goName = "";

        [SerializeField]
        float connectionTimeOut = 15f;
        bool tryingToConnect = false;

        void Start()
        {
            goName = gameObject.name;

            switch (goName)
            {
                case "BeginnerGround":
                    roomIndex = 0;
                    break;

                case "Town":
                    roomIndex = 1;
                    break;

                case "DeathValley":
                    roomIndex = 2;
                    break;

                case "OldCemetery":
                    roomIndex = 3;
                    break;

                case "MiseryField":
                    roomIndex = 4;
                    break;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            //! Give player some debug message
            if (!PhotonManager.Instance.readyToConnect && !PhotonManager.Instance.isInRoom)
            {
                tryingToConnect = true;
                Debug.Log($"Player is in {goName}");
                return;
            }

            if(PhotonManager.Instance.readyToConnect && PhotonManager.Instance.isInRoom)
            {
                if(Photon.Pun.PhotonNetwork.CurrentRoom.Name == gameObject.name)
                {
                    return;
                }
            }

            if (other.transform.parent.CompareTag("Player"))
            {
                if(plController == null)
                    plController = other.gameObject.GetComponentInParent<PhotonPlayerController>();
                if (plController != null && plController.isOtherPlayer)
                {
                    return;
                }
                else if(plController != null && !plController.isOtherPlayer)
                {
                    PhotonController.Instance.JoinOrCreateRoom(roomIndex);
                   
                }
                
            }
        }

        public void OnTriggerExit(Collider other)
        {
            //! Disable this for now
            return;

            //! Give player some debug message
            if (PhotonManager.Instance.readyToConnect && !PhotonManager.Instance.isInRoom)
                return;

            if (other.transform.parent.CompareTag("Player"))
            {
                plController = other.gameObject.GetComponentInParent<PhotonPlayerController>();
                if (plController != null && plController.isOtherPlayer)
                {
                    return;
                }
                else if (plController != null && !plController.isOtherPlayer)
                {
                    Debug.Log("Calling leave room");
                    PhotonController.Instance.LeaveRoom();

                    //! reset connection timeout
                    connectionTimeOut = 15f;
                    tryingToConnect = false;
                }
            }
        }

        /// <summary>
        /// We will try to reconnect if connection is ready
        /// </summary>
        /// <param name="other"></param>
        public void OnTriggerStay(Collider other)
        {
            if(PhotonManager.Instance.readyToConnect && !PhotonManager.Instance.isInRoom)
            {
                if (!tryingToConnect)
                    return;
                if(connectionTimeOut >= 0f)
                    connectionTimeOut -= Time.deltaTime * 1f;

                if (connectionTimeOut <= 0)
                {
                    if (other.transform.parent.CompareTag("Player"))
                    {
                        if (plController == null)
                            plController = other.gameObject.GetComponentInParent<PhotonPlayerController>();

                        if (plController != null && plController.isOtherPlayer)
                        {
                            return;
                        }
                        else if (plController != null && !plController.isOtherPlayer)
                        {
                            PhotonController.Instance.JoinOrCreateRoom(roomIndex);
                        }
                    }
                    connectionTimeOut = 15f;
                }
            }
        }
    }
}