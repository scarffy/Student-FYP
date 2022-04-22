using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FYP.UI
{
    public class PlayerName : MonoBehaviour
    {
        #region private variable
        [Header("private variable")]
        [SerializeField] string playerName;
        [SerializeField] Camera camera;

        [Header("UI")]
        [SerializeField] GameObject canvas;
        [SerializeField] TextMeshProUGUI nameText;
        #endregion

        void Start()
        {
            if (camera == null) camera = Camera.main;

            if (Backend.PlayFabManager.Instance.isSignIn)
            {
                SetName();
            }

            if (string.IsNullOrEmpty(playerName))
            {
                canvas.SetActive(false);
                Backend.UserAccountController.Instance.OnFoundInfo += SetName;
            }
            else
            {
                canvas.SetActive(true);
            }
        }

        private void Update()
        {
            gameObject.transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        }

        void SetName()
        {
            SetName(Data.PlayfabAccountInfo.Instance.accountInfo.TitleInfo.DisplayName);
        }

        public void SetName(string value)
        {
            playerName = value;
            nameText.text = playerName;
            canvas.SetActive(true);
        }

        private void OnDestroy()
        {
            Backend.UserAccountController.Instance.OnFoundInfo -= SetName;
        }
    }
}