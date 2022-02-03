using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// For trigger an event while an object stay in the area
/// </summary>

namespace FYP.Common
{
    public class CheckColliderStay : MonoBehaviour
    {
        public UnityEvent<string> GameObjectEnterName;
        public UnityEvent GameObjectEnter;
        public UnityEvent<Collider> GameObjectEnterCollider;

        public void OnTriggerStay(Collider other)
        {
            GameObjectEnterName?.Invoke(other.gameObject.name);
            GameObjectEnter?.Invoke();
            GameObjectEnterCollider?.Invoke(other);
        }
    }
}
