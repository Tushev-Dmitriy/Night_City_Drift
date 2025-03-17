using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

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

        YG2.GameReadyAPI();
        SceneManager.UnloadSceneAsync("Init");
    }
}
