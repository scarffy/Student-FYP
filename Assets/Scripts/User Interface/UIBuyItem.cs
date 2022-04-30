using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FYP.UI
{
    public class UIBuyItem : MonoBehaviour
    {
        [SerializeField] private CatalogItem instance;

        /// <summary>
        /// This also has currency <Dictionary>
        /// Check on get-catalog-items from playfab
        /// </summary>
        public CatalogItem Instance 
        {
            get => instance;
            set
            {
                instance = value;

                //! Set item image
                itemName.text = Instance.DisplayName;
                if(Instance.VirtualCurrencyPrices.TryGetValue("KC",out uint Value))
                {
                    itemPrice.SetText("KC" + string.Format("{0:n0}", Value));
                }
            }
        }

        [SerializeField] Button button;

        [Space(20)]
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI itemName;
        [SerializeField] TextMeshProUGUI itemPrice;

        // Start is called before the first frame update
        void Start()
        {
            button.onClick.AddListener(() => OpenBuyDetails());
        }

        public void SetItemInstance(CatalogItem item) => Instance = item;

        void OpenBuyDetails() => UIShopBuy.Instance.SetDetail(this);
    }
}