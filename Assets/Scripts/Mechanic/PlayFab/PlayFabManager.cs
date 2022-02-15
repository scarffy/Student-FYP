using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using PlayFab;
using PlayFab.ClientModels;

namespace FYP.Backend
{
    public class PlayFabManager : Singleton<PlayFabManager>
    {

        [Header("Virtual Currency")]
        public int KC = 0;

        [Header("Coin")]
        public TMP_Text coinText;

        [Header("Stock Price")]
        public uint cost;

        [Header("Info Request")]
        public GetPlayerCombinedInfoRequestParams infoRequest;
    }
}

