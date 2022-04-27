using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayFab;
using PlayFab.ClientModels;

namespace FYP.Backend{ 
    public class PlayfabInventorySystem 
    {
        /// <summary>
        /// Get the latest player inventory
        /// </summary>
        /// <param name="successCallback"></param>
        /// <param name="errorCallback"></param>
        public void GetInventory(Action<List<ItemInstance>> successCallback = null, Action errorCallback = null,Action<int> kaching = null)
        {
            GetUserInventoryRequest request = new GetUserInventoryRequest();
            PlayFabClientAPI.GetUserInventory(request, result => {
                if (successCallback != null) successCallback(result.Inventory);
                int value;
                if(result.VirtualCurrency.TryGetValue("KC",out value) && kaching != null)
                {
                    kaching(value);
                }
            }, error => {
                if (errorCallback != null) errorCallback();
            });
        }

        /// <summary>
        /// Sell Item. Using cloud script
        /// </summary>
        public void SellItem(string itemId,Action<string> successCallback = null, Action<string> errorCallback = null)
        {
                       
        }

        public void GetCatalogItems(Action<List<CatalogItem>> callback = null)
        {
            var request = new GetCatalogItemsRequest
            {
                
            };
            PlayFabClientAPI.GetCatalogItems(
                request
                ,Result => {
                    if(callback != null)
                    {
                        callback(Result.Catalog);
                    }
                }, 
                Err => { Debug.LogError(Err.GenerateErrorReport()); }) ;
        }

        #region Currency
        public void AddCurrency(int amount,Action<int> successCallback = null, Action<string> errorCallback = null)
        {
            var request = new AddUserVirtualCurrencyRequest
            {
                Amount = amount,
                VirtualCurrency = "KC",
            };
            
            PlayFabClientAPI.AddUserVirtualCurrency(request, res => {
                if (successCallback != null) successCallback(res.Balance);
            }, err => {
                if (errorCallback != null) errorCallback(err.GenerateErrorReport());
            });
        }

        public void MinusCurrency(int amount, Action<int> successCallback = null, Action<string> errorCallback = null)
        {
            var request = new SubtractUserVirtualCurrencyRequest
            {
                Amount = amount,
                VirtualCurrency = "KC"
            };
            PlayFabClientAPI.SubtractUserVirtualCurrency(request, res =>
            {
                if (successCallback != null) successCallback(res.Balance);
            }, err => 
            {
                if (errorCallback != null) errorCallback(err.GenerateErrorReport());
            });
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId">Item Instance Id</param>
        /// <param name="consumeValue">Consume Value</param>
        /// <param name="susCB">Success Callback (Can be Null). Return Item Instance and Item Remaining Uses</param>
        /// <param name="errCB">Error Callback (Can be Null)</param>
        public void ConsumeItem(string itemId,int consumeValue,
            System.Action<string,int> successCallback = null, System.Action<string> errorCallback = null)
        {
            var req = new ConsumeItemRequest
            {
                ItemInstanceId = itemId,
                ConsumeCount = consumeValue,
            };
            PlayFabClientAPI.ConsumeItem(
                req, 
                res => {
                    if (successCallback != null) successCallback(res.ItemInstanceId,res.RemainingUses);
                }, 
                err => {
                    if(errorCallback != null)
                    {
                        errorCallback(err.GenerateErrorReport());
                    }
                });
        }
    }
}