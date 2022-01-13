using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FYP
{
    public class LevelManager : Singleton<LevelManager>
    {
        public enum CurrentLoadedScene
        {
            Core,
            Credit,
            RegisterLogin,
            Gameplay
        }

        public CurrentLoadedScene currentLoadedScene;

        void Awake()
        {
           
        }

        void Start()
        {
            SceneManager.sceneLoaded += LoadScene;
        }

        public void LoadNextLevel()
        {
            Debug.Log("Called load next level");
        }

        void LoadScene(Scene scene, LoadSceneMode mode)
        {

        }
    }
}