using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

namespace FYP.Backend
{
    /// <summary>
    /// Inventory system as the manager
    /// </summary>
    public class InventorySystem : Singleton<InventorySystem>
    {
        // Prefab <- want
        public GameObject buttonObject;
        // Spawn list?
        public List<GameObject> inventoryObjects;

        // What does this do?
        public Item[] Items;
        public GameObject[] enableGameObject; //this will be enabled when user login the account
        [Header("User Interface")]
        // UI <- Dont want
        public GameObject contentArea;
        // UI <- Dont want
        public GameObject inventoryContent;
        
        public List<int> inventoryStacks;
        // UI < Dont want
        public GameObject inventoryBeg;
        // UI < Dont want
        public GameObject shopBag;

        [Header("New Stuffs")]
        public System.Action<List<ItemInstance>> OnUpdatedInventory;
        public System.Action<int> OnUpdateKaChing;

        #region Gift
        public void BasicInventory()
        {
            PurchaseItemRequest request = new PurchaseItemRequest();
            request.CatalogVersion = "Items";
            request.ItemId = "BasicInventory";
            request.VirtualCurrency = "KC";
            request.Price = 0;
            PlayFabClientAPI.PurchaseItem(request, result =>
            {
                Backend.PlayFabManager.Instance.KC += (int)result.Items[0].UnitPrice;
            }, error =>
            {
                Debug.LogError(error.ErrorMessage);
            });
        }
        #endregion

        #region Request Price from Catalog
        public void GetItemPrice()
        {
            GetCatalogItemsRequest request = new GetCatalogItemsRequest();
            request.CatalogVersion = "Items";
            PlayFabClientAPI.GetCatalogItems(request, result =>
            {
                List<CatalogItem> items = result.Catalog;
                foreach (CatalogItem i in items)
                {
                    uint cost = i.VirtualCurrencyPrices["KC"];
                    foreach (Item editorItems in Items)
                    {
                        //if (editorItems.Name == i.ItemId)
                        //{
                        //    editorItems.Cost = (int)cost;
                        //}
                    }
                    //Debug.Log(cost);
                }

                foreach (Item i in Items)
                {
                    GameObject o = Instantiate((buttonObject), contentArea.transform.position, Quaternion.identity);
                    //o.transform.GetChild(0).GetComponent<TMP_Text>().text = i.Name;
                    //o.transform.GetChild(1).GetComponent<TMP_Text>().text = "[" + i.Cost + "]";
                    o.GetComponent<Image>().sprite = i.GetComponent<SpriteRenderer>().sprite;
                    o.GetComponent<Image>().preserveAspect = true;
                    o.transform.SetParent(contentArea.transform);
                    //o.GetComponent<Button>().onClick.AddListener(delegate { MakePurchase(i.Name, i.Cost); });
                }
            }, error => { });

        }
        #endregion

        #region Purchase Item
        void MakePurchase(string name, int price)
        {
            PurchaseItemRequest request = new PurchaseItemRequest();
            request.CatalogVersion = "Items";
            request.ItemId = name;
            request.VirtualCurrency = "KC";
            request.Price = price;

            PlayFabClientAPI.PurchaseItem(request, result =>
            {
                GetInventory();
                Backend.PlayFabManager.Instance.KC -= price;
            }, error =>
            {
                Debug.Log(error.ErrorMessage);
            });
        }
        #endregion

        #region UpdateInventory
        public void GetInventory()
        {
            PlayfabInventorySystem inv = new PlayfabInventorySystem();
            inv.GetInventory(res =>
            {
                OnUpdatedInventory(res);
            }, 
            null,
            k => { 
                OnUpdateKaChing?.Invoke(k);
                PlayFabManager.Instance.KC = k;
            });
        }
        #endregion

        #region Virtual Currency
        /// <summary>
        /// this is to display Virtual Currency into game scene
        /// Call KC from playfab manager
        /// </summary>
        void Update()
        {
            Backend.PlayFabManager.Instance.coinText.text = "Coin : " + Backend.PlayFabManager.Instance.KC;

            //if (Input.GetKeyUp(KeyCode.Alpha0))
            //{
            //    ConsumeItem();
            //}
            //if (Input.GetKeyUp(KeyCode.Alpha9))
            //{
            //    PlayfabInventorySystem r = new PlayfabInventorySystem();
            //    r.AddCurrency(50);
            //}
        }
        #endregion

        #region Consume Items
        public void ConsumeItem()
        {
            PlayfabInventorySystem inv = new PlayfabInventorySystem();
            inv.ConsumeItem("FD94AE77D0E41761", 1, ConsumeItemResult, err => {
                Debug.LogError($"Consume item error : {err}");
            });
        }

        void ConsumeItemResult(string itemName, int remainingValue)
        {
            Debug.LogError($"Item consume success : {itemName} has {remainingValue} left");
        }

        public void SellItem(string itemId)
        {
            PlayfabInventorySystem inv = new PlayfabInventorySystem();
            inv.SellItem(itemId);
        }
        #endregion
    }
}


