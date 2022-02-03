using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// For trigger an event when an object enter an area
/// </summary>

namespace FYP.Common
{
    public class CheckColliderEnter : MonoBehaviour
    {

        public UnityEvent<string> GameObjectEnterName;
        public UnityEvent GameObjectEnter;
        public UnityEvent<Collider> GameObjectEnterCollider;

        public void OnTriggerEnter(Collider other)
        {
            GameObjectEnterName?.Invoke(other.gameObject.name);
            GameObjectEnter?.Invoke();
            GameObjectEnterCollider?.Invoke(other);
        }
    }
}
