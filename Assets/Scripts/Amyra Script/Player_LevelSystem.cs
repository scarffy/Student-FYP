using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_LevelSystem : MonoBehaviour
{
    public static Player_LevelSystem instance;

    private LevelSystem levelSystem;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum Equip
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

        SetHealthBarSize(1f + levelSystem.GetLevelNumber() * .1f);
    }


    //!healthbar

    private void SetHealthBarSize(float healthBarSize)
    {
        GameObject.Find("healthBar1").transform.localScale = new Vector3(.7f * healthBarSize, 1, 1);
    }

    public void SetEquip(Equip equip)
    {
        //Texture2D texture = new Texture2D(512, 512, TextureFormat.RGBA32, true);

        //Color[] spritesheetBasePixels = baseTexture.GetPixels(0, 0, 512, 512);
        //texture.SetPixel(0, 0, 512, 512, spritesheetBasePixels);

        //Color[] headPixels;
        //switch (equip)
        //{
        //    default:
        //    case equip.None:
        //        Debug.Log("LevelNone");
        //        //headPixels = headTexture.GetPixels(0, 0, 128, 128);
        //        break;
        //    case equip.Helmet_1:
        //        Debug.Log("Level5");
        //        //headPixels = head1Texture.GetPixels(0, 0, 128, 128);
        //        break;
        //    case equip.Helmet_2:
        //        Debug.Log("Level10");
        //        //headPixels = head2Texture.GetPixels(0, 0, 128, 128);
        //        break;

        //}
        //texture.SetPixel(4, 384, 128, 128, headPixels);
        //texture.Apply();
        //material.mainTexture = texture;

    }

}
