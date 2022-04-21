using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private LevelWindow levelWindow;
    [SerializeField] private Player_LevelSystem player;
    //[SerializeField] private EquipWindow equipWindow;

    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        
        levelWindow.SetLevelSystem(levelSystem);
        player.SetLevelSystem(levelSystem);
        //equipWindow.SetLevelSystem(levelSystem);

        LevelSystem_Animator levelSystem_Animator = new LevelSystem_Animator(levelSystem);
    }
}
