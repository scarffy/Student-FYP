using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FYP.Backend
{
    public class PickUpItem : Singleton<PickUpItem>
    {
		public float TheDistance;
		public GameObject theKey;
		public GameObject ActionDisplay;
		public GameObject ActionText;

		void Update()
		{
			TheDistance = PlayerRaycast.DistanceFromTarget;
		}

		void OnMouseOver()
		{
			if (TheDistance <= 2)
			{
				ActionDisplay.SetActive(true);
				ActionText.SetActive(true);
			}
			if (Input.GetButtonDown("Action"))
			{
				if (TheDistance <= 2)
				{

					this.GetComponent<BoxCollider>().enabled = false;
					ActionDisplay.SetActive(false);
					ActionText.SetActive(false);
					theKey.SetActive(false);
					//redirected item to inventory list
					//update inventory list 
				}
			}
		}

		void OnMouseExit()
		{
			ActionDisplay.SetActive(false);
			ActionText.SetActive(false);

			
		}
	}
}

