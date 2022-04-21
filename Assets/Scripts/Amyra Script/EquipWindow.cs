using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipWindow : MonoBehaviour
{
    [SerializeField] private Player_LevelSystem player_levelSystem;

    private LevelSystem levelSystem;

    private void Awake()
    {
        //transform.Find("equipNoneBtn").GetComponent<Button>().onClick.AddListener(() => { });
        //transform.Find("equipHelmet1Btn").GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    if (levelSystem.GetLevelNumber() >= 4)
        //    {
        //        player_levelSystem.SetEquip(player_levelSystem.SetEquip().Helmet_1);

        //    }
        //});
        //transform.Find("equipHelmet2tn").GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    if (levelSystem.GetLevelNumber() >= 9)
        //    {
        //        player_levelSystem.SetEquip(player_levelSystem.SetEquip().Helmet_2);
        //    }
        //});
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

    

}
