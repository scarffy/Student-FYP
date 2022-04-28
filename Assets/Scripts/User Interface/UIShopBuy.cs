using FYP.Backend;
using PlayFab.ClientModels;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.UI
{
    /// <summary>
    /// This will get all catalog items 
    /// Compare and get appropriate catalog
    /// </summary>
    public class UIShopBuy : MonoBehaviour
    {
        public List<CatalogItem> items = new List<CatalogItem>();

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKeyUp(KeyCode.Alpha0))
            //{
            //    Backend.PlayfabInventorySystem cat = new Backend.PlayfabInventorySystem();
            //    cat.GetCatalogItems(OnGetCatalogItems);
            //}
            //if (Input.GetKeyUp(KeyCode.Alpha9))
            //{
            //    GetItemsByTag("Healing");
            //}
            if (Input.GetKeyUp(KeyCode.E))
            {
                GetCatalogByTag("Healing");
            }
        }

        #region Get Catalog
        public void GetCatalogByTag(string tag)
        {
            PlayfabInventorySystem cat = new PlayfabInventorySystem();
            cat.GetCatalogItemsByTag(tag,OnGetCatalogItems);
        }

        void OnGetCatalogItems(List<CatalogItem> obj)
        {
            items = new List<CatalogItem>();
            foreach (var item in obj)
            {
                items.Add(item);
            }
        }
        #endregion

        /// <summary>
        /// Check buyer's money before buy
        /// </summary>
        public void PurchaseItem(string itemId,int itemPrice)
        {
            InventorySystem inv = new InventorySystem();
            inv.BuyItem(itemId, itemPrice);
        }
    }
}