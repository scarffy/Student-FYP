using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_LevelSystem : MonoBehaviour
{
    private LevelSystem levelSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;

    }

    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        Debug.Log("Yeay Victory");

        //SetHealthBarSize(1f + levelSystem.GetLevelNumber() * .1f);
    }


    //!healthbar

    //private void SetHealthBarSize(float healthBarSize)
    //{
    //  transform.Find("healthBar1").localScale = new Vector3(.7f * healthBarSize, 1, 1);
    //}

}
