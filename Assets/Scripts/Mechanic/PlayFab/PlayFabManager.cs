using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using PlayFab;
using PlayFab.ClientModels;

namespace FYP.Backend
{
    public class PlayFabManager : MonoBehaviour
    {
        public void SignUp()
        {
            UserAccountController.Instance.OnTryRegisterNewAccount();
        }

    }
}

