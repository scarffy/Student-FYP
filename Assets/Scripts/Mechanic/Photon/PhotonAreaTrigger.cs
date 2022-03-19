using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.Backend
{
    public class PhotonAreaTrigger : MonoBehaviour
    {
        public PhotonPlayerController pController;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log($"Player entering zone {gameObject.name}");

                PhotonController.Instance.roomIndex = 0;
                PhotonController.Instance.JoinOrCreateRoom();
            }
        }
    }
}