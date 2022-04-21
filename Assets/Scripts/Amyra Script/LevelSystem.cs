using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelSystem //: MonoBehaviour
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private static readonly int[] experiencePerLevel = new[] { 100, 120, 140, 160, 180, 200, 250, 300, 400 };
    private int level;
    private int experience;
    //private int experienceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        //experienceToNextLevel = 100;
    }

    //im changing to while but it didnt go to the level 2
    public void AddExperience(int amount)
    {
        if (!IsMaxLevel())
        {
            experience += amount;
            while (!IsMaxLevel() && experience >= GetExperienceToNextLevel(level))
            {
                //Enough experience to level up                
                experience -= GetExperienceToNextLevel(level);
                level++;

                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            }
            if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExperienceNormalized()
    {
        if (IsMaxLevel())
        {
            return 1f;

        }
        else
        {
            return (float)experience / GetExperienceToNextLevel(level);
        }
        
    }

    public int GetExperience()
    {
        return experience;
    }

    public int GetExperienceToNextLevel(int level)
    {
        //return experienceToNextLevel;
        //!infinite loop level calculation
        //return level * 10;
        if (level < experiencePerLevel.Length)
        {
            return experiencePerLevel[level];

        }
        else
        {
            //invalid level
            Debug.LogError("Level invalid" + level);
            return 100;
        }

        //
       
    }

    public bool IsMaxLevel()
    {
        return IsMaxLevel(level);
    }

    public bool IsMaxLevel(int level)
    {
        return level == experiencePerLevel.Length - 1;
    }

}
