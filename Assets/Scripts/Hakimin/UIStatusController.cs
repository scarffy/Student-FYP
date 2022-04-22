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

        public void SetStatus(Backend.PlayerData data)
        { 
            health.text = data.playerHealth.ToString();
            strength.text = data.playerHealth.ToString();
            vitality.text = data.playerVitality.ToString();
            experience.text = data.currentEXP.ToString() + " / " + data.maxEXP.ToString();
        }

        public void SetStatusPlayerName(string value)
        {
            playerName.text = value;
        }
    }
}