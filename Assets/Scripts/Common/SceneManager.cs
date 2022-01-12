using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace FYP
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;

        public enum CurrentLoadedScene
        {
            Core,
            Credit,
            RegisterLogin,
            Gameplay
        }

        public CurrentLoadedScene currentLoadedScene;

        // Start is called before the first frame update
        void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += LoadedScene;
        }

        public void LoadNextScene()
        {

        }

        void LoadedScene(Scene scene, LoadSceneMode mode)
        {
            
        }
    }
}