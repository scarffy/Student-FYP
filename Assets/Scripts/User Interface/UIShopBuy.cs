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
    public class UIShopBuy : Singleton<UIShopBuy>
    {
        public List<CatalogItem> items = new List<CatalogItem>();

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.E)) GetCatalogByTag(ShopTrigger.ShopType.Food);
        }

        #region Get Catalog
        public void GetCatalogByTag(ShopTrigger.ShopType tag)
        {
            PlayfabInventorySystem cat = new PlayfabInventorySystem();
            cat.GetCatalogItemsByTag(tag.ToString(), OnGetCatalogItems);
        }
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

        #region Instantiate Items
        void PopulateUI()
        {

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