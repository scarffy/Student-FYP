using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;

namespace FYP.Data
{
    /// <summary>
    /// This is under Data namespace since it store data.
    /// This is data that we get from playfab
    /// </summary>
    public class PlayfabAccountInfo : Singleton<PlayfabAccountInfo>
    {
        [Header("PlayFab Account Info")]
        public UserAccountInfo accountInfo;

        [Header("Info Request")]
        public GetPlayerCombinedInfoRequestParams infoRequest;


        public static void FillData(UserAccountInfo value)
        {
            Instance.accountInfo = value;

            UserLocalSaveFile.Instance.saveData.playfabId = value.PlayFabId;
            UserLocalSaveFile.Instance.saveData.displayName = value.TitleInfo.DisplayName;

            UserLocalSaveFile.Instance.SaveData();
            
        }

        public static void FillData(UserAccountInfo value, Action action)
        {
            Instance.accountInfo = value;

            UserLocalSaveFile.Instance.saveData.playfabId = value.PlayFabId;
            UserLocalSaveFile.Instance.saveData.displayName = value.TitleInfo.DisplayName;

            UserLocalSaveFile.Instance.SaveData();
            action();
        }

    }
}