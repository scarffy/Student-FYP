using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.UI
{
    public class PlayerName : MonoBehaviour
    {
        #region private variable
        [Header("private variable")]
        [SerializeField] string playerName;
        [SerializeField] Camera camera;
        #endregion

        void Start()
        {
            if (camera == null) camera = Camera.main;
        }

        private void Update()
        {
            gameObject.transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        }

        public void SetName(string value)
        {
            playerName = value;
        }
    }
}