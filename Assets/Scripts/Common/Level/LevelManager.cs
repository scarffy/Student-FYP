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
        public List<string> levelsToLoad;
        public List<string> levelsToUnload;

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
        /// If there is specific scene to load. Use this for large scenes
        /// </summary>
        /// <param name="levelName"></param>
        /// <param name="mode">Default value is additive</param>
        void LoadLevels(string levelName = null, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            if (levelName == null) return;

            SceneManager.LoadSceneAsync(levelName, mode);
        }

        /// <summary>
        /// Core loading and unloading scenes that use scriptable objects
        /// </summary>
        /// <param name="levelData"></param>
        void LoadLevels(LevelData levelData)
        {
            AddLevelToLoad(levelData);
            AddLevelToUnload(levelData);

            foreach (var item in levelData.levels)
            {
                if (CheckIfLoaded(item.name))
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

        #region Core Loading and Unloading Scenes
        /// <summary>
        /// Add to level to load list and will check if level already loaded
        /// </summary>
        /// <param name="levelData"></param>
        void AddLevelToLoad(LevelData levelData)
        {
            levelsToLoad.Clear();
            foreach (var item in levelData.levels)
            {
                levelsToLoad.Add(item.name);
            }
        }

        bool CheckIfLoaded(string levelName)
        {
            for (int i = 0; i < levelsToLoad.Count; i++)
            {
                if (levelsToLoad[i] == levelName)
                    return true;
            }

            return false;
        }

        void AddLevelToUnload(LevelData levelData)
        {
            levelsToUnload.Clear();
            for (int i = 0; i < currentLevelsLoaded.Count; i++)
            {
                levelsToUnload.Add(currentLevelsLoaded[i]);
            }

            //! Add all level data into level to unload
            foreach (var item in levelData.levels)
            {
                if (levelsToLoad.Contains(item.name))
                {
                    levelsToUnload.Remove(item.name);
                }
            }

            foreach (var item in levelsToUnload)
            {
                UnloadScene(item);
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

        #endregion
    }
}