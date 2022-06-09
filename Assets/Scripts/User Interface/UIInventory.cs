using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FYP.UI
{
    public class UIInventory : Singleton<UIInventory>
    {
        [SerializeField] GameObject invContent;
        [SerializeField] GameObject invButtonPrefab;

        [SerializeField] List<GameObject> invItemList = new List<GameObject>();

        [SerializeField] TextMeshProUGUI kachingText;

        private void Awake()
        {
            Backend.InventorySystem.Instance.OnUpdatedInventory += OnInventoryUpdate;
            Backend.InventorySystem.Instance.OnUpdateKaChing += OnKaChingUpdate;
        }

        /// <summary>
        /// Update the inventory
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
                Backend.Item item = go.GetComponent<Backend.Item>();
                invItemList.Add(go);

                item.ItemName = itemList[i].DisplayName;
                item.SetItemInstance(itemList[i].ItemInstanceId);
                item.ItemPrice = (int)itemList[i].UnitPrice;
                item.ItemStack = itemList[i].RemainingUses;
                Data.InventoryImagesArray.Instance.FetchID(int.Parse(itemList[i].ItemId));
                item.SetItemImage(Data.InventoryImagesArray.Instance.temporarySprite);
                
            }
        }

        void OnKaChingUpdate(int value)
        {
            kachingText.text = string.Format("{0:n0}", value);
        }
    }
}