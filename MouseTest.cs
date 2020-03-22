using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseTest : MonoBehaviour
{
    private void Awake()
    {
    }

    void Start ()
    {
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SteamVR_LoadLevel.Begin("Mountain");
            LoadingScene.LoadScene("Mountain");
        }
    }
}
