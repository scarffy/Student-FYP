using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayFab;
using PlayFab.ClientModels;

namespace FYP.Backend
{
    [System.Serializable]
    public class MonsterStatus
    {
        public MonsterData[] items;
    }

    [System.Serializable]
    public class MonsterData
    {
        public string monsterName;
        public int monsterLevel;
        public int monsterHealth;
        public int monsterEXP;
        public int monsterAttack;
        public int monsterVitality;
    }

    public class MonsterStats : Singleton<MonsterStats>
    {
        public MonsterStatus monsterStatus;

        public void GetTitleData()
        {
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest()
            {
                Keys = new List<string>() { "MonsterData" }
            },
            result =>
            {
                string json = result.Data["MonsterData"];
                monsterStatus = JsonUtility.FromJson<MonsterStatus>(json);
            },
            error =>
            {
                Debug.Log("Got error retrieving monster data:");
                Debug.Log(error.GenerateErrorReport());
            }
            );
        }
    }
}

