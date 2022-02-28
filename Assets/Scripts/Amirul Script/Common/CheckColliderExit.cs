using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FYP.Common
{
    public class CheckColliderExit : MonoBehaviour
    {
        public UnityEvent<string> GameObjectEnterName;
        public UnityEvent GameObjectEnter;
        public UnityEvent<Collider> GameObjectEnterCollider;

        public void OnTriggerExit(Collider other)
        {
            GameObjectEnterName?.Invoke(other.gameObject.name);
            GameObjectEnter?.Invoke();
            GameObjectEnterCollider?.Invoke(other);
        }
    }
}
