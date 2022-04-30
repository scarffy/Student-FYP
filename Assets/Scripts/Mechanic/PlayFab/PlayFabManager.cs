using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using PlayFab;
using PlayFab.ClientModels;

namespace FYP.Backend
{
    /// <summary>
    /// Everthing non related to playfab should only refer to this script
    /// Everything related to playfab should update to this script
    /// </summary>
    public class PlayFabManager : Singleton<PlayFabManager>
    {
        [Header("Virtual Currency")]
        public int KC = 0;

        [Header("Checker")]
        public bool isSignIn;
    }
}

