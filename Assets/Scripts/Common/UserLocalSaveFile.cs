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

        public void SaveData()
        {
            string tempString = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(saveName, tempString);
        }

        public void LoadData()
        {
            string tempString = "";
            if (PlayerPrefs.HasKey(saveName))
            {
                tempString = PlayerPrefs.GetString(saveName);
                saveData = JsonUtility.FromJson<LocalSaveFile>(tempString);
            }
        }
    }

    [Serializable]
    public class LocalSaveFile
    {
        public string username;
        public string playfabId;

        [Space]
        public string displayName;
    }
}