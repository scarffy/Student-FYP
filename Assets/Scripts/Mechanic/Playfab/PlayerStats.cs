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
                Debug.Log("Got user data: ");
                Debug.Log("Current EXP : " + result.Data["CurrentEXP"].Value);
                Debug.Log("Max EXP : " + result.Data["MaxEXP"].Value);
                Debug.Log("Player Health : " + result.Data["PlayerHealth"].Value);
                Debug.Log("Player Strength : " + result.Data["PlayerStrength"].Value);
                Debug.Log("Player Level : " + result.Data["PlayerLevel"].Value);
                Debug.Log("Player Vitality : " + result.Data["PlayerVitality"].Value);
                //if (result.Data == null || !result.Data.ContainsKey("CurrentEXP")) Debug.Log("No CurrentEXP");
                //else Debug.Log("MaxEXP: " + result.Data["MaxEXP"].Value); 
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
            result => Debug.Log("Successfully updated user data"),
            error =>
            {
                Debug.Log("Got error setting user statistic data");
                Debug.Log(error.GenerateErrorReport());
            });
        }


    }
}

