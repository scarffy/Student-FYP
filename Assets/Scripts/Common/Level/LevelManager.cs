using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FYP.Data;
using TMPro;

namespace FYP
{   /// <summary>
    /// To do : Add coroutine loading
    /// Debug why it wont load in Runtime. Not reference to an object? in LoadAllLevels. Why?
    /// </summary>
    public class LevelManager : Singleton<LevelManager>
    {
        public enum CurrentLoadedScene
        {
            Core,
            GameUi,
            Single,
            Multiplayer
        }

        public CurrentLoadedScene currentLoadedScene;

        [Space]
        public LevelData[] allLevels;

        [Header("Level loaded")]
        public List<string> currentLevelsLoaded;
        public List<string> levelsToLoad;
        public List<string> levelsToUnload;


        void Start()
        {
            currentLoadedScene = CurrentLoadedScene.Core;
            Debug.LogError(allLevels[0].levelsName[0]);
            //LoadEditorNextLevel();
            LoadNextLevel();
        }

        /// <summary>
        /// If there is no specific scene to load. Use this.
        /// </summary>
        public void LoadEditorNextLevel()
        {
            switch (currentLoadedScene)
            {
                case CurrentLoadedScene.Core:
                    LoadEditorLevels(allLevels[0]);
                    currentLoadedScene = CurrentLoadedScene.Single;
                    break;

                case CurrentLoadedScene.Single:
                    LoadEditorLevels(allLevels[1]);
                    break;
            }
        }

        public void LoadNextLevel()
        {
            switch (currentLoadedScene)
            {
                case CurrentLoadedScene.Core:
                    LoadLevels(allLevels[0]);
                    currentLoadedScene = CurrentLoadedScene.Single;
                    break;

                case CurrentLoadedScene.Single:
                    LoadLevels(allLevels[2]);
                    currentLoadedScene = CurrentLoadedScene.Multiplayer;
                    break;

                case CurrentLoadedScene.Multiplayer:
                    LoadLevels(allLevels[2]);
                    break;
            }
        }

        /// <summary>
        /// If there is specific scene to load. Use this for large scenes
        /// </summary>
        /// <param name="levelName"></param>
        /// <param name="mode">Default value is additive</param>
        public void LoadLevels(string levelName = null, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            if (levelName == null) return;

            SceneManager.LoadSceneAsync(levelName, mode);
        }

        /// <summary>
        /// Core loading and unloading scenes that use scriptable objects.
        /// This use object which is not recognize in build
        /// </summary>
        /// <param name="levelData"></param>
        void LoadEditorLevels(LevelData levelData)
        {
            AddEditorLevelToLoad(levelData);
            AddEditorLevelToUnload(levelData);

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

        /// <summary>
        /// Core loading and unloading scenes that use string
        /// </summary>
        /// <param name="levelData"></param>
        void LoadLevels(LevelData levelData)
        {
            Debug.Log("Calling Loadlevels");
            AddLevelToLoad(levelData);
            AddLevelToUnload(levelData);

            foreach (var item in levelData.levelsName)
            {
                if (CheckIfLoaded(item))
                {
                    Scene loadedLevel = SceneManager.GetSceneByName(item);

                    if (!loadedLevel.isLoaded)
                    {
                        SceneManager.LoadSceneAsync(item, LoadSceneMode.Additive);
                        currentLevelsLoaded.Add(item);
                    }
                }
            }
        }

        #region Core Loading and Unloading Scenes
        /// <summary>
        /// Add to level to load list and will check if level already loaded
        /// </summary>
        /// <param name="levelData"></param>
        void AddEditorLevelToLoad(LevelData levelData)
        {
            levelsToLoad.Clear();
            foreach (var item in levelData.levels)
            {
                levelsToLoad.Add(item.name);
            }
        }

        void AddLevelToLoad(LevelData levelData)
        {
            levelsToLoad.Clear();
            foreach (var item in levelData.levelsName)
            {
                levelsToLoad.Add(item);
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

        void AddEditorLevelToUnload(LevelData levelData)
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

        void AddLevelToUnload(LevelData levelData)
        {
            levelsToUnload.Clear();
            for (int i = 0; i < currentLevelsLoaded.Count; i++)
            {
                levelsToUnload.Add(currentLevelsLoaded[i]);
            }

            foreach (var item in levelData.levelsName)
            {
                if (levelsToUnload.Contains(item))
                {
                    levelsToUnload.Remove(item);
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