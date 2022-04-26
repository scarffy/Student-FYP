using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FYP.Backend
{
    /// <summary>
    /// This affect GetInventory function
    /// This affect GetItemPrice function
    /// </summary>
    public class Item : MonoBehaviour
    {
        [SerializeField] private string itemName;
        public string ItemName
        {
            get => itemName;
            set
            {
                itemName = value;
                itemNameText.text = value;
            }
        }

        [SerializeField] private int itemPrice;
        public int ItemPrice {
            get => itemPrice;
            set {
                itemPrice = value;
                //itemPriceText.text = value.ToString(); 
            }
        }

        public int GainPerSecond; // <-- ??

        public int? itemStack;
        public int? ItemStack
        {
            get => itemStack;
            set
            {
                itemStack = value;
                itemStackText.text = value.ToString();
                if (value == null) SetItemStack(false);
                else SetItemStack(true);
            }
        }

        [SerializeField] string itemInstanceId;

        [SerializeField] TextMeshProUGUI itemNameText;
        [SerializeField] TextMeshProUGUI itemPriceText;
        [SerializeField] TextMeshProUGUI itemStackText;


        public void SetItemInstance(string value) => itemInstanceId = value;
        void SetItemStack(bool value) => itemStackText.transform.parent.gameObject.SetActive(value);
    }
}

