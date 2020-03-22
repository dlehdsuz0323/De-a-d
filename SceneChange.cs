using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Tag")
        {
            if (GameSupervisor.Instance.isTutorial && GameSupervisor.Instance.isHandTutorial[3])
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("ReporterHouse");
            }
        }
    }
}
