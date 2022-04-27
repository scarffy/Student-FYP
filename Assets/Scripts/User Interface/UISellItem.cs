using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        public int? itemStack;
        public int? ItemStack
        {
            get => itemStack;
            set
            {
                itemStack = value;
            }
        }

        [SerializeField] private string itemClass;
        public string ItemClass
        {
            get => itemClass; 
            set { itemClass = value; }
        }

        public string itemInstanceId;

        [SerializeField] TextMeshProUGUI itemNameText;
        public Sprite sprite;
        [SerializeField] Image image;

        [SerializeField] Button button;

        private void Start()
        {
            button.onClick.AddListener(() => { OpenSetDetails(); });
        }

        public void SetItemInstance(string value) => itemInstanceId = value;
        public void SetImage()
        {
            image.sprite = sprite;
        }

        void OpenSetDetails() =>  UIShopSell.Instance.SetDetails(this);
    }
}