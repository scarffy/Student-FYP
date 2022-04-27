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

        public void GetItemsByTag(string tag)
        {
            foreach(var item in items)
            {
                if (!GetTags(item.Tags,tag))
                {
                    items.Remove(item);
                }
            }
        }
    }
}