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
        #region move these to UI scripts
        [Header("Coin")]
        public TMP_Text coinText;

        [Header("Stock Price")]
        public uint cost;

        [Header("Player Stat")]
        public TMP_Text currentEXP;
        public TMP_Text maxEXP;
        public TMP_Text playerLevel;
        public TMP_Text playerHealth;
        public TMP_Text playerVitality;
        public TMP_Text playerStrength;
        #endregion

        [Header("Player Update Btn")]
        public GameObject playerStats;

        [Header("Checker")]
        public bool isSignIn;

    }
}

