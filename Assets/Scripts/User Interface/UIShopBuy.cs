using FYP.Backend;
using PlayFab.ClientModels;
using TMPro;
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
        [Header("Catalog")]
        public List<CatalogItem> items = new List<CatalogItem>();
        public List<GameObject> itemInvList;

        [Space(20)]
        [SerializeField] GameObject shopInvContent;
        [SerializeField] GameObject shopButtonPrefab;
        [SerializeField] GameObject shopDetailPanel;

        [Space(20)]
        string _itemId;
        int _itemPrice;
        [SerializeField] TextMeshProUGUI itemTitle;
        [SerializeField] TextMeshProUGUI itemCategory;
        [SerializeField] TextMeshProUGUI itemDescription;
        [SerializeField] TextMeshProUGUI itemPrice;

        [Header("User Interface (UI)")]
        [SerializeField] TextMeshProUGUI kachingText;

        [Header("Purchase Status")]
        [SerializeField] GameObject purchasePanel;
        [SerializeField] TextMeshProUGUI purchaseStatusText;

        void Start()
        {
            InventorySystem.Instance.OnUpdateKaChing += UpdateMoney;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.E)) {
                UIStateManager.Instance.SetState(UIStateManager.State.buy);
                GetCatalogByTag(ShopTrigger.ShopType.Healing);
            }
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
            PopulateUI();
        }
        #endregion

        #region Instantiate Items
        void PopulateUI()
        {
            if(itemInvList != null)
            {
                foreach (var item in itemInvList)
                {
                    Destroy(item);
                }
                itemInvList.Clear();
            }

            for (int i = 0; i < items.Count; i++)
            {
                GameObject go = Instantiate(shopButtonPrefab, shopInvContent.transform);
                UIBuyItem item = go.GetComponent<UIBuyItem>();
                item.Instance = items[i];
            }
        }

        void UpdateMoney(int value)
        {
            kachingText.text = string.Format("{0:n0}", value);
        }
        #endregion

        /// <summary>
        /// Check buyer's money before buy
        /// </summary>
        public void PurchaseItem()
        {
            InventorySystem.Instance.BuyItem(_itemId, _itemPrice);

        }

        public void SetDetail(UIBuyItem obj)
        {
            shopDetailPanel.SetActive(true);
            itemTitle.text = obj.Instance.DisplayName;
            itemCategory.text = string.IsNullOrEmpty(obj.Instance.ItemClass) ? obj.Instance.ItemClass : "Null";
            itemDescription.text = string.IsNullOrEmpty(obj.Instance.Description) ? obj.Instance.Description : "Null";
            _itemId = obj.Instance.ItemId;

            if(obj.Instance.VirtualCurrencyPrices.TryGetValue("KC",out uint value))
            {
                itemPrice.text = "KC "+ value.ToString();
                _itemPrice = (int)value;
            }
        }
    }
}