using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class BackToGarage : MonoBehaviour
{
    [SerializeField] EventManager _eventManager;

    public void GoToGarage()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        AsyncOperation gameScene = SceneManager.UnloadSceneAsync("Game");

        YG2.InterstitialAdvShow();

        while (!gameScene.isDone)
        {
            yield return null;
        }

        _eventManager.SetupGarage();
    }
}
