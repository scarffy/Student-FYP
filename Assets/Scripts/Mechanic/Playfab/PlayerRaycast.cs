using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.Backend
{
	public class PlayerRaycast : Singleton<PlayerRaycast>
	{
		public static float DistanceFromTarget;
		public float ToTarget;

		void Update()
		{
			RaycastHit Hit;
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit))
			{
				ToTarget = Hit.distance;
				DistanceFromTarget = ToTarget;
			}
		}
	}

}
