using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

namespace FYP.Backend
{
    public class InventorySystem : Singleton<InventorySystem>
    {
        public Item[] Items;
        public void BuyItem()
        {

            GetCatalogItemsRequest request = new GetCatalogItemsRequest();
            request.CatalogVersion = "Sword";
            PlayFabClientAPI.GetCatalogItems(request, result => {
                List<CatalogItem> items = result.Catalog;
                foreach(CatalogItem i in items)
                {
                    PlayFabManager.Instance.cost = i.VirtualCurrencyPrices["KC"];
                    Debug.Log(PlayFabManager.Instance.cost);
                }
            }, error => { });
        }
    }
}


