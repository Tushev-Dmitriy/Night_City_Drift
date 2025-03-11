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
        AsyncOperation uiScene = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        
        while (!uiScene.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync("Init");
    }
}
