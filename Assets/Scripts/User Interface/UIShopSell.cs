using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab.ClientModels;

namespace FYP.UI
{
    public class UIShopSell : Singleton<UIShopSell>
    {
        [SerializeField] GameObject invContent;
        [SerializeField] GameObject invButtonPrefab;

        [SerializeField] List<GameObject> invItemList = new List<GameObject>();

        [SerializeField] TextMeshProUGUI kachingText;

        [Header("Detail Panels")]
        [SerializeField] GameObject detailPanel;

        void Awake()
        {
            Backend.InventorySystem.Instance.OnUpdatedInventory += OnInventoryUpdate;
            Backend.InventorySystem.Instance.OnUpdateKaChing += OnKaChingUpdate;
        }

        /// <summary>
        /// Update Inventory list for selling
        /// </summary>
        /// <param name="itemList"></param>
        void OnInventoryUpdate(List<ItemInstance> itemList)
        {
            if (invItemList != null)
            {
                foreach (var item in invItemList)
                {
                    Destroy(item);
                }
                invItemList.Clear();
            }

            for (int i = 0; i < itemList.Count; i++)
            {
                GameObject go = Instantiate(invButtonPrefab, invContent.transform);
                UISellItem item = go.GetComponent<UISellItem>();
                invItemList.Add(go);

                item.ItemName = itemList[i].DisplayName;
                item.SetItemInstance(itemList[i].ItemInstanceId);
                item.ItemPrice = (int)itemList[i].UnitPrice;
                item.ItemStack = itemList[i].RemainingUses;
                item.ItemClass = itemList[i].ItemClass;
  
            }
        }

        void OnKaChingUpdate(int value)
        {
            kachingText.text = string.Format("{0:n0}", value);
        }

        public void SetDetails(UISellItem item)
        {
            detailPanel.SetActive(true);
        }
    }
}