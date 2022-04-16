using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


namespace FYP.Backend
{
    public class PlayerStats : Singleton<PlayerStats>
    {
        //public UserDataPermission userData;
        public int playerLevel;
        public int playerStrength;
        public int playerHealth;
        public int playerVitality;
        public int currentEXP;
        public int maxEXP;

        public void GetUserData(string myPlayFabId)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            {
                PlayFabId = myPlayFabId,
                Keys = null
            }, result =>
            {
                Backend.PlayFabManager.Instance.playerLevel.text = "Level : " + result.Data["PlayerLevel"].Value;
                Backend.PlayFabManager.Instance.playerHealth.text = "Health : " + result.Data["PlayerHealth"].Value;
                Backend.PlayFabManager.Instance.playerVitality.text = "Vitality : " + result.Data["PlayerVitality"].Value;
                Backend.PlayFabManager.Instance.playerStrength.text = "Strength : " + result.Data["PlayerStrength"].Value;
                Backend.PlayFabManager.Instance.currentEXP.text = "Current Exp : " + result.Data["CurrentEXP"].Value;
                Backend.PlayFabManager.Instance.maxEXP.text = " Max Exp : " + result.Data["MaxEXP"].Value;
            }, (error) =>
            {
                Debug.Log("Got error retrieving user data:");
                Debug.Log(error.GenerateErrorReport());
            });
        }

        public void SetUserData()
        {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>() {
            {"CurrentEXP", "100"},
            {"MaxEXP", "200"},
            {"PlayerHealth", "100"},
            {"PlayerVitality", "50"},
            {"PlayerStrength", "100"},
            {"PlayerLevel", "1"}

        }
            },
            result =>
            {
                Debug.Log("Successfully updated user data");
            },
            error =>
            {
                Debug.Log("Got error setting user statistic data");
                Debug.Log(error.GenerateErrorReport());
            });
        }


    }
}

