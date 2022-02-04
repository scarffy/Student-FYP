using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// To : add UI
/// </summary>
namespace FYP
{
    public class EditorLevelDebugController : MonoBehaviour
    {
        [SerializeField] string[] sceneNames;

        private void OnEnable()
        {
            #if !UNITY_EDITOR
                //Destroy(gameObject);
            #endif
        }

        void Start()
        {
            var sceneNumber = SceneManager.sceneCountInBuildSettings;
            sceneNames = new string[sceneNumber];

            for (int i = 0; i < sceneNumber; i++)
            {
                sceneNames[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }
        }

        public void Button_ChooseLevel(int value)
        {
            if(value < sceneNames.Length)
            {
                LevelManager.Instance.LoadLevels(sceneNames[value]);
            }
        }
    }
}