using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace FYP.Combat
{
    public class LevelWindow : MonoBehaviour
    {
        private TextMeshProUGUI levelText;
        private Image experienceBarImage;
        private LevelSystem levelSystem;

        private void Awake()
        {
            levelText = transform.Find("levelText").GetComponent<TextMeshProUGUI>();
            experienceBarImage = transform.Find("experienceBar").Find("bar").GetComponent<Image>();

            //SetExperienceBarSize(.5f);
            //SetLevelNumber(7);        
        }



        private void SetExperienceBarSize(float experienceNormalized)
        {
            experienceBarImage.fillAmount = experienceNormalized;
        }

        private void SetLevelNumber(int levelNumber)
        {
            levelText.text = "Level\n" + (levelNumber + 1);
        }

        public void SetLevelSystem(LevelSystem levelSystem)
        {
            // set the levelsystem object
            this.levelSystem = levelSystem;

            // update the starting values
            SetLevelNumber(levelSystem.GetLevelNumber());
            SetExperienceBarSize(levelSystem.GetExperienceNormalized());

            // subscribe to the changed events
            levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
            levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;

        }

        private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
        {
            // level changed, update text
            SetLevelNumber(levelSystem.GetLevelNumber());
        }

        private void LevelSystem_OnExperienceChanged(object sender, EventArgs e)
        {
            // experience changed, update bar size
            SetExperienceBarSize(levelSystem.GetExperienceNormalized());
        }
    }
}