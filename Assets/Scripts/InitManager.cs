using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        AsyncOperation gameScene = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        AsyncOperation uiScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        
        while (!gameScene.isDone && !uiScene.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync("Init");
    }
}
