using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FYP.Data;

namespace FYP
{   /// <summary>
    /// To do : Add coroutine loading
    /// </summary>
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
        public LevelData[] allLevels;

        [Header("Level loaded")]
        public List<string> currentLevelsLoaded;

        void Awake()
        {
           
        }

        void Start()
        {
            currentLoadedScene = CurrentLoadedScene.Core;
            
            LoadNextLevel();
        }

        /// <summary>
        /// If there is no specific scene to load. Use this.
        /// </summary>
        public void LoadNextLevel()
        {
            switch (currentLoadedScene)
            {
                case CurrentLoadedScene.Core:
                    LoadLevels(allLevels[0]);
                    break;
            }
        }

        /// <summary>
        /// If there is specific scene to load. Use this.
        /// </summary>
        /// <param name="levelName"></param>
        /// <param name="mode">Default value is additive</param>
        void LoadLevels(string levelName = null, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            if (levelName == null) return;

            SceneManager.LoadSceneAsync(levelName, mode);
        }

        void LoadLevels(LevelData levelData)
        {
            foreach (var item in levelData.levels)
            {
                if (!CheckIfLoaded(item.name))
                {
                    Scene loadedLevel = SceneManager.GetSceneByName(item.name);
                    // Check if level is loaded before the load it
                    if (!loadedLevel.isLoaded)
                    {
                        SceneManager.LoadSceneAsync(item.name, LoadSceneMode.Additive);
                        currentLevelsLoaded.Add(item.name);
                    }
                }
            }
        }

        public void UnloadScene(string levelName = null)
        {
            if (levelName == null) return;
            SceneManager.UnloadSceneAsync(levelName);

            for (int i = 0; i < currentLevelsLoaded.Count; i++)
            {
                if(currentLevelsLoaded[i] == levelName)
                {
                    currentLevelsLoaded.Remove(levelName);
                }
            }
        }

        bool CheckIfLoaded(string levelName)
        {
            for (int i = 0; i < currentLevelsLoaded.Count; i++)
            {
                if (currentLevelsLoaded[i] == levelName)
                    return true;
            }

            return false;
        }
    }
}