using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.Data
{
    public class InventoryImagesArray : Singleton<InventoryImagesArray>
    {
        public InventoryImages_SO[] inventoryImages = new InventoryImages_SO[10];

        private void Start()
        {

        }

        public Sprite temporarySprite;



        public void FetchID(int ID)
        {
            for (int i = 0; i < inventoryImages.Length; i++)
            {
                if (inventoryImages[i].id == ID)
                {
                    temporarySprite = inventoryImages[i].inventorySprite;
                    return;
                }
                Debug.Log(ID);

            }
            temporarySprite = null;
        }
    }
}
