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
            Debug.Log("Diary");
            //! Register name in playfab
            //! If not sign in / sign up, then stop player and open sign up or sign in panel
            if(!FYP.Backend.PlayFabManager.Instance.isSignIn){
                UIStateManager.Instance.SetState(UIStateManager.State.signin);
            }
        }
    }
}
