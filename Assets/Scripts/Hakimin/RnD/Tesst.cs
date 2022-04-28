using PlayFab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Tesst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AddVC()
    {
        PlayFabServerAPI.AddUserVirtualCurrency(new PlayFab.ServerModels.AddUserVirtualCurrencyRequest
        {
            Amount = 500,
            PlayFabId = "607B1CC771DF5F51",
            VirtualCurrency = "KC"
        },
        OnAddSuccess => {
            Debug.Log(OnAddSuccess.Balance);
        },
        OnFailure
        );
    }

    void RevokeServerTest()
    {
        PlayFabServerAPI.RevokeInventoryItem(new PlayFab.ServerModels.RevokeInventoryItemRequest
        {
            PlayFabId = "607B1CC771DF5F51",
            ItemInstanceId = "10E3743C450EFF6F"
        },
        OnRevokeSuccess => { },
        OnFailure);
    }

    void OnFailure(PlayFabError obj)
    {
        Debug.LogError(obj.GenerateErrorReport());
    }
}
