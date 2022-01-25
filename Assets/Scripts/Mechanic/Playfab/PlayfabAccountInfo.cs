using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using PlayFab.ClientModels;

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

        public static void FillData(UserAccountInfo value)
        {
            Instance.accountInfo = value;

            UserLocalSaveFile.Instance.saveData.playfabId = value.PlayFabId;
            UserLocalSaveFile.Instance.saveData.displayName = value.TitleInfo.DisplayName;
        }

        public static void FillData(UserAccountInfo value, Action action)
        {
            Instance.accountInfo = value;

            UserLocalSaveFile.Instance.saveData.playfabId = value.PlayFabId;
            UserLocalSaveFile.Instance.saveData.displayName = value.TitleInfo.DisplayName;

            action();
        }
    }
}