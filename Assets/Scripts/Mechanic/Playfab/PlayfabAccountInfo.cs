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

        //[Header("PlayerAction Stats")]
        //public int playerLevel;
        //public int playerStrength;
        //public int playerHealth;
        //public int playerVitality;
        //public int currentEXP;
        //public int maxEXP;


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

        #region playerstats
        //public void SetStats()
        //{

        //    PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        //    {
        //        Statistics = new List<StatisticUpdate>
        //        {
        //            new StatisticUpdate { StatisticName = "PlayerLevel", Value = playerLevel},
        //            new StatisticUpdate { StatisticName = "PlayerStrength", Value = playerStrength},
        //            new StatisticUpdate { StatisticName = "PlayerHealth", Value = playerHealth},
        //            new StatisticUpdate { StatisticName = "PlayerVitality", Value = playerVitality},
        //            new StatisticUpdate { StatisticName = "CurrentEXP", Value = currentEXP},
        //            new StatisticUpdate { StatisticName = "MaxEXP", Value = maxEXP},
        //        }
        //    },
        //    result =>
        //    {
        //        Debug.Log("User statistic updated");
        //        Backend.PlayFabManager.Instance.playerLevel.text = "Level : " + playerLevel;
        //        Backend.PlayFabManager.Instance.playerHealth.text = "Health : " + playerHealth;
        //        Backend.PlayFabManager.Instance.playerVitality.text = "Vitality : " + playerVitality;
        //        Backend.PlayFabManager.Instance.playerStrength.text = "Strength : " + playerStrength;
        //        Backend.PlayFabManager.Instance.currentEXP.text = "Current Exp : " + currentEXP;
        //        Backend.PlayFabManager.Instance.maxEXP.text = " Max Exp : " + maxEXP;
        //    },
        //    error => { Debug.LogError(error.GenerateErrorReport()); });
        //}
        //public void GetStatistics()
        //{
        //    PlayFabClientAPI.GetPlayerStatistics(
        //        new GetPlayerStatisticsRequest(),
        //        OnGetStatistics,
        //        error => Debug.LogError(error.GenerateErrorReport())
        //    );
        //}

        //public void OnGetStatistics(GetPlayerStatisticsResult result)
        //{

        //    Debug.Log("Received the following Statistics:");

        //    foreach (var eachStat in result.Statistics)
        //    {
        //        Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);

        //        switch (eachStat.StatisticName)
        //        {
        //            case "PlayerLevel":
        //                playerLevel = eachStat.Value;
        //                break;
        //            case "PlayerHealth":
        //                playerHealth = eachStat.Value;
        //                break;
        //            case "PlayerStrength":
        //                playerStrength = eachStat.Value;
        //                break;
        //            case "PlayerVitality":
        //                playerVitality = eachStat.Value;
        //                break;
        //            case "CurrentEXP":
        //                currentEXP = eachStat.Value;
        //                break;
        //            case "MaxEXP":
        //                maxEXP = eachStat.Value;
        //                break;
        //        }

        //    }
        //}

        //void Update()
        //{
        //    Exp();

        //    //later kalau pick up item, exp naik 10 or dapat pape reward la 
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        currentEXP += 100;
        //        Debug.Log("Current EXP : " + currentEXP);
        //    }
        //}

        //void Exp()
        //{
        //    if (currentEXP >= maxEXP)
        //        LevelUp();
        //}

        //void LevelUp()
        //{
        //    playerLevel += 1;
        //    currentEXP = 0;

        //    switch (playerLevel)
        //    {
        //        case 2:
        //            //playerHealth = 200;
        //            maxEXP = 200;
        //            break;

        //        case 3:
        //            // playerHealth = 300;
        //            maxEXP = 300;
        //            print("Congratulations! You have hit Level3 on your character!");
        //            break;

        //        case 4:
        //            maxEXP = 400;
        //            print("You WIN!");
        //            //gi Winner Scene
        //            break;
        //    }

        //}

        #endregion
    }
}