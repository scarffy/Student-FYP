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
            GameUi,
            Credit,
            RegisterLogin,
            Gameplay
        }

        public CurrentLoadedScene currentLoadedScene;

        [Space]
        public Scene scene1;

        void Awake()
        {
           
        }

        void Start()
        {
            SceneManager.sceneLoaded += LoadScene;
        }

        /// <summary>
        /// If there is no specific scene to load, use this
        /// </summary>
        public void LoadNextLevel()
        {
            Debug.Log("Called load next level");
            //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


            //switch (currentLoadedScene)
            //{
            //    case CurrentLoadedScene.Core:

            //        break;
            //}
        }

        /// <summary>
        /// If there is specific scene to load, use this
        /// </summary>
        /// <param name="value"></param>
        public void LoadNextLevel(int value)
        {

        }

        void LoadScene(Scene scene, LoadSceneMode mode)
        {

        }
    }
}