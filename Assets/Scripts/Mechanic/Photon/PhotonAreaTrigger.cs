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
            

            if (other.transform.parent.CompareTag("Player"))
            {
                plController = other.gameObject.GetComponentInParent<PhotonPlayerController>();
                if (plController != null && plController.isOtherPlayer)
                {
                    Debug.Log("Not local player");
                    return;
                }
                else if(plController != null && !plController.isOtherPlayer)
                {
                    Debug.Log("Local player");
                    PhotonController.Instance.roomIndex = 0;
                    PhotonController.Instance.JoinOrCreateRoom();
                }
            }
        }
    }
}