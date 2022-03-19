using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.Backend
{
    public class PhotonAreaTrigger : MonoBehaviour
    {
        public PhotonPlayerController plController;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log($"Player entering zone {gameObject.name}");
                plController = other.GetComponent<PhotonPlayerController>();
                if (plController!= null && plController.isOtherPlayer)
                {
                    return;
                }

                PhotonController.Instance.roomIndex = 0;
                PhotonController.Instance.JoinOrCreateRoom();
            }
        }
    }
}