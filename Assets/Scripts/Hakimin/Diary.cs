using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FYP;
using FYP.UI;

public class Diary : Singleton<Diary>
{
    [SerializeField] StarterAssets.StarterAssetsInputs player;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!FYP.Backend.PlayFabManager.Instance.isSignIn){
                player = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
                player.IsUiOn = true;
                UIStateManager.Instance.SetState(UIStateManager.State.signin);
            }
        }
    }
}
