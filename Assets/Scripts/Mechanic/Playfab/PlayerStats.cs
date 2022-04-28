using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


namespace FYP.Backend
{
    [System.Serializable]
    public class PlayerData
    {
        [Header("Player Stats")]
        public int playerLevel;
        public int playerStrength;
        public int playerHealth;
        public int playerVitality;
        public int currentEXP;
        public int maxEXP;
    }
    public class PlayerStats : Singleton<PlayerStats>
    {
        public PlayerData playerData;
        public System.Action<PlayerData> _playerData;

        #region getdata
        public void GetUserData(string myPlayFabId)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            {
                PlayFabId = myPlayFabId,
                Keys = null
            }, result =>
            {
                string json = result.Data["PlayerData"].Value;
                playerData = JsonUtility.FromJson<PlayerData>(json);
                _playerData?.Invoke(playerData);
            }, (error) =>
            {
                Debug.Log("Got error retrieving user data:");
                Debug.Log(error.GenerateErrorReport());
            });
        }

        public void GetUserData(string myPlayFabId, string key, System.Action callback = null,System.Action<string> callbackString = null)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            {
                PlayFabId = myPlayFabId,
                Keys = null
            },
            result => {
                string json = result.Data["PlayerData"].Value;
                playerData = JsonUtility.FromJson<PlayerData>(json);
                if (result.Data.ContainsKey(key))
                {
                    if (callback != null)
                        callback();
                    if (callbackString != null)
                        callbackString(result.Data[key].Value);
                }
            },
            error =>
            {

            });
        }

        #endregion

        #region setdata
        /// <summary>
        /// Set PlayerData key
        /// </summary>
        public void SetUserData()
        {
            string json = JsonUtility.ToJson(playerData);
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>() {
                    {"PlayerData", json }
                },
                Permission = UserDataPermission.Public,
            },
            result =>
            {
                Debug.Log("Successfully updated user data");
            },
            error =>
            {
                Debug.Log("Got error setting user statistic data");
                Debug.Log(error.GenerateErrorReport());
            }); ;
        }

        /// <summary>
        /// Generic set any data with callbacks
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="callback"></param>
        public void SetUserData(string key, string value, System.Action callback = null)
        {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                Data = new Dictionary<string, string>
                {
                    { key,value }
                },
                Permission = UserDataPermission.Public,
            },
            result => {
                if (callback != null) callback();
            },
            error => {

            });
        }
        #endregion
    }
}

