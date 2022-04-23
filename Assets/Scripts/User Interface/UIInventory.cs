using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.UI
{
    public class UIInventory : Singleton<UIInventory>
    {
        [SerializeField] GameObject invContent;
        [SerializeField] GameObject invButtonPrefab;

        [SerializeField] List<GameObject> invItemList = new List<GameObject>();

        private void Awake()
        {
            Backend.InventorySystem.Instance.OnUpdatedInventory += OnInventoryUpdate;
        }

        /// <summary>
        /// Update the inventory
        /// </summary>
        /// <param name="itemList"></param>
        void OnInventoryUpdate(List<ItemInstance> itemList)
        {
            if(invItemList != null)
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
                Backend.Item item = go.GetComponent<Backend.Item>();

                item.ItemName = itemList[i].DisplayName;
                item.SetItemInstance(itemList[i].ItemInstanceId);
                item.ItemPrice = (int)itemList[i].UnitPrice;
                item.ItemStack = itemList[i].RemainingUses;
            }
        }
    }
}