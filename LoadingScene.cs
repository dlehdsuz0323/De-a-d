using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static string nextScene;
    static private float time;

    private float FadeInTime = 3.0f;
    static private float FadeOutTime = 0.5f;

    private bool LoadingSceneFadeIn = false;
    private bool LoadingSceneFadeOut = false;
    static string nextSceneName;
    static AsyncOperation ls;

    private void Start()
    {
        ls.allowSceneActivation = false;
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        time = 0;

        nextScene = sceneName;
        ls = SceneManager.LoadSceneAsync("LoadingScene");
        //ls.allowSceneActivation = true;
        //SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        //yield return null;

        if (!LoadingSceneFadeOut)
        {
            StartCoroutine(FadeOut());
            Debug.Log("Fade out");
            LoadingSceneFadeOut = true;
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        if (!LoadingSceneFadeIn)
        {
            StartCoroutine(FadeIn());
            Debug.Log("Fade In");
            LoadingSceneFadeIn = true;
        }

        while(!ls.isDone)
        {
            yield return null;
            if (ls.progress >= 0.9f)
            {
                ls.allowSceneActivation = true;
            }
        }

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("FadeOutIn");

                    StartCoroutine(FadeOutIn());

                    op.allowSceneActivation = true;
                }
            }
            else
            {
            }
        }
    }
    IEnumerator FadeIn()
    {
        time = 0;

        SteamVR_Fade.Start(Color.clear, FadeInTime);

        while (time < FadeInTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        time = 0;

        SteamVR_Fade.Start(Color.black, FadeOutTime);

        while (time < FadeOutTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOutIn()
    {
        StartCoroutine(FadeOut());
        StartCoroutine(FadeIn());
        yield return null;
    }
    IEnumerator FadeInOut()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut());
        yield return null;
    }
}
