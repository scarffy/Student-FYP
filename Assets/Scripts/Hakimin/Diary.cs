using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FYP.UI;

public class Diary : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Diary");
            //! Register name in playfab
            //! If not sign in / sign up, then stop player and open sign up or sign in panel
            UIStateManager.Instance.SetState(UIStateManager.State.signup);
        }
    }
}
