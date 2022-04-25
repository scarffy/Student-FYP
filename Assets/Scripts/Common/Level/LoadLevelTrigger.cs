using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP
{
    public class LoadLevelTrigger : MonoBehaviour
    {
        public void LoadNextLevel()
        {
            Debug.Log("Calling instance");
            LevelManager.Instance.LoadNextLevel();
        }
    }
}