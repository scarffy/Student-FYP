using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FYP.Data
{
    /// <summary>
    /// Save unimportant files to local device or maybe any files that need to be used later in the local network
    /// To do : 
    /// 1. Make a struct to share with network
    /// </summary>
    public class UserLocalSaveFile : Singleton<UserLocalSaveFile>
    {
        /// <summary>
        /// Add data in class if needed
        /// </summary>
        public LocalSaveFile saveData;

        const string saveName = "saveName";

        [HideInInspector]
        public string saveDataString;

        public void SaveData(LocalSaveFile data)
        {
            saveDataString = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(saveName, saveDataString);
        }

        public void SaveData()
        {
            saveDataString = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(saveName, saveDataString);

            Debug.Log("Save Local Data");
        }

        public void LoadData()
        {
            saveDataString = "";
            if (PlayerPrefs.HasKey(saveName))
            {
                saveDataString = PlayerPrefs.GetString(saveName);
                saveData = JsonUtility.FromJson<LocalSaveFile>(saveDataString);
            }
            else
            {
                Debug.Log("No local save file found");
            }
        }
    }

    [Serializable]
    public class LocalSaveFile
    {
        public string username;
        public string playfabId;
        //public string virtualCurrency;

        [Space]
        public string displayName;
    }
}