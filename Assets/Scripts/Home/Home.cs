using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    private AsyncOperation loader;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        // StartCoroutine(LoadGameScene());
    }

    private void Update()
    {
        // if (loader != null)
        // {
        //     Debug.Log(loader.progress);
        // }
    }

    // IEnumerator LoadGameScene()
    // {
    //     loader = SceneManager.LoadSceneAsync("Game");
    //     loader.allowSceneActivation = true;
    //     if (!loader.isDone)
    //     {
    //         Debug.Log(loader.progress);
    //         yield return null;
    //     }
    // }
}