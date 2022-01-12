using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static LoadingScene Instance;

    public enum CurrentLoadedScene
    {
        None,
        Credit,
        Register,
        Level1
    }

    public CurrentLoadedScene currentLoadedScene = CurrentLoadedScene.None;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void LoadNextScene(int value)
    {

    }
}
