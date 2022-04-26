using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FYP.UI
{
    /// <summary>
    /// Repurpose to fit Sell button
    /// </summary>
    public class UISellItem : MonoBehaviour
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
        public int ItemPrice
        {
            get => itemPrice;
            set
            {
                itemPrice = value;
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
            }
        }

        [SerializeField] string itemInstanceId;

        [SerializeField] TextMeshProUGUI itemNameText;
        [SerializeField] TextMeshProUGUI itemPriceText;
        [SerializeField] TextMeshProUGUI itemStackText;


        public void SetItemInstance(string value) => itemInstanceId = value;
    }
}