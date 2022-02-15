using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

namespace FYP.Backend
{
    public class InventorySystem : Singleton<InventorySystem>
    {
        public Item[] Items;
        public GameObject[] enableGameObject; //this will be enabled when user login the account
        public GameObject contentArea;
        public GameObject buttonObject;
        public GameObject inventoryContent;
        private List<GameObject> inventoryObjects = new List<GameObject>();

        public void BasicInventory()
        {
            PurchaseItemRequest request = new PurchaseItemRequest();
            request.CatalogVersion = "Items";
            request.ItemId = "BasicInventory";
            request.VirtualCurrency = "KC";
            request.Price = 0;
            PlayFabClientAPI.PurchaseItem(request, result => {
                Backend.PlayFabManager.Instance.KC += (int)result.Items[0].UnitPrice;
            }, error => {
                Debug.LogError(error.ErrorMessage);
            });
        }
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
                        if (editorItems.Name == i.ItemId)
                        {
                            editorItems.Cost = (int)cost;
                        }
                    }
                    Debug.Log(cost);
                }

                foreach (Item i in Items)
                {
                    GameObject o = Instantiate((buttonObject), contentArea.transform.position, Quaternion.identity);
                    o.transform.GetChild(0).GetComponent<TMP_Text>().text = i.Name;
                    o.transform.GetChild(1).GetComponent<TMP_Text>().text = "[" + i.Cost + "]";
                    o.GetComponent<Image>().sprite = i.GetComponent<SpriteRenderer>().sprite;
                    o.GetComponent<Image>().preserveAspect = true;
                    o.transform.SetParent(contentArea.transform);
                    o.GetComponent<Button>().onClick.AddListener(delegate { MakePurchase(i.Name, i.Cost); });
                }
            }, error => { });

        }

        void MakePurchase(string name, int price)
        {
            PurchaseItemRequest request = new PurchaseItemRequest();
            request.CatalogVersion = "Items";
            request.ItemId = name;
            request.VirtualCurrency = "KC";
            request.Price = price;

            PlayFabClientAPI.PurchaseItem(request, result =>
            {
                UpdateInventory();
                Backend.PlayFabManager.Instance.KC -= price;
            }, error =>
            {
                Debug.Log(error.ErrorMessage);
            });
        }


        public void UpdateInventory()
        {
            GetUserInventoryRequest request = new GetUserInventoryRequest();

            PlayFabClientAPI.GetUserInventory(request, result =>
            {
                if (inventoryObjects != null)
                {
                    foreach(GameObject obj in inventoryObjects)
                    {
                        Destroy(obj);
                    }
                    inventoryObjects.Clear();
                }
                List<ItemInstance> ii = result.Inventory;
                foreach (ItemInstance i in ii)
                {
                    foreach (Item editorI in Items)
                    {
                        if (editorI.Name == i.ItemId)
                        {
                            GameObject o = Instantiate((buttonObject), inventoryContent.transform.position, Quaternion.identity);
                            o.transform.GetChild(0).GetComponent<TMP_Text>().text = i.ItemId;
                            o.transform.GetChild(1).GetComponent<TMP_Text>().text = "[" + editorI.Cost + "]";
                            o.GetComponent<Image>().sprite = editorI.GetComponent<SpriteRenderer>().sprite;
                            o.GetComponent<Image>().preserveAspect = true;
                            o.transform.SetParent(inventoryContent.transform);
                            inventoryObjects.Add(o);
                        }

                    }

                }
            }, error =>
            {

            });
        }

        void Update()
        {
            Backend.PlayFabManager.Instance.coinText.text = "Coin : " + Backend.PlayFabManager.Instance.KC;
        }
    }
}


