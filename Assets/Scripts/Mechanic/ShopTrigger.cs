using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
  [SerializeField] StarterAssetsInputs player;

  public void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      StarterAssetsInputs controller =  other.GetComponent<StarterAssetsInputs>();
      if(controller.playerController != null)
      {
        if (!controller.playerController.isOtherPlayer)
        {
          // Can open shop UI from starter input
          // If Press E. Then open UI
        }
      }
      else
      {
        Debug.LogWarning("Missing controller reference. Is this intended?");
      }
    }
  }
}
