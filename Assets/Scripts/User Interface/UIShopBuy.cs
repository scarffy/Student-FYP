using PlayFab.ClientModels;
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
            if (Input.GetKeyUp(KeyCode.Alpha0))
            {
                Backend.PlayfabInventorySystem cat = new Backend.PlayfabInventorySystem();
                cat.GetCatalogItems(OnGetCatalogItems);
            }
            if (Input.GetKeyUp(KeyCode.Alpha9))
            {
                GetItemsByTag("Healing");
            }
        }

        void OnGetCatalogItems(List<CatalogItem> obj)
        {
            items = new List<CatalogItem>();
            foreach (var item in obj)
            {
                items.Add(item);
            }
        }

        bool GetTags(List<string> tags,string tag)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if(tags[i] == tag)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get items by tag
        /// </summary>
        /// <param name="tag"></param>
        public void GetItemsByTag(string tag)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if(!GetTags(items[i].Tags, tag))
                {
                    items.Remove(items[i]);
                }
            }
        }
    }
}