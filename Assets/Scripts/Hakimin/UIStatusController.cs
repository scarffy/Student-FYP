using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FYP.UI
{
    public class UIStatusController : Singleton<UIStatusController>
    {
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI health;
        public TextMeshProUGUI strength;
        public TextMeshProUGUI vitality;
        public TextMeshProUGUI experience;

        public string _playerName;

        public string GetPlayerName
        {
            get {
                return Data.PlayfabAccountInfo.Instance.accountInfo.TitleInfo.DisplayName;
            }
            set {
                _playerName = value;
            }
        }

        public void SetStatus(Backend.PlayerData data)
        {
            _playerName = GetPlayerName;
            playerName.text = _playerName;
            health.text = data.playerHealth.ToString();
            strength.text = data.playerHealth.ToString();
            vitality.text = data.playerVitality.ToString();
            experience.text = data.currentEXP.ToString() + " / " + data.maxEXP.ToString();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.I))
            {
                UIStateManager.Instance.SetState(UIStateManager.State.status);
            }
        }
    }
}