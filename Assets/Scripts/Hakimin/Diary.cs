using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FYP;
using FYP.UI;

public class Diary : Singleton<Diary>
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!FYP.Backend.PlayFabManager.Instance.isSignIn){
                UIStateManager.Instance.SetState(UIStateManager.State.signin);
            }
        }
    }
}
