//using System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem_Animator // : MonoBehaviour
{
    private LevelSystem levelSystem;
    private bool isAnimating;

    private int level;
    private int experience;
    //private int experienceToNextLevel;

    public LevelSystem_Animator(LevelSystem levelSystem)
    {
        SetLevelSystem(levelSystem);
        
    }

    private void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        level = levelSystem.GetLevelNumber();
        experience = levelSystem.GetExperience();
        //experienceToNextLevel = levelSystem.GetExperienceToNextLevel();

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;

    }

    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        isAnimating = false;
    }

    private void LevelSystem_OnExperienceChanged(object sender, EventArgs e)
    {
        isAnimating = true;
    }

    private void Update()
    {
        if (isAnimating)
        {
            if (level < levelSystem.GetLevelNumber())
            {
                //Local level under target level
                AddExperience();
            }
            else
            {
                //Local level equals the target level
                if (experience < levelSystem.GetExperience())
                {
                    AddExperience();
                }
                else
                {
                    isAnimating = false; 
                }
            }

        }
        Debug.Log(level + " " + experience);
    }

    private void AddExperience()
    {
        experience++;
        if (experience >= levelSystem.GetExperienceToNextLevel(level)) 
        {
            level++;
            experience = 0;

        }
    }
}
