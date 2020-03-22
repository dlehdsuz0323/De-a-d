using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPuzzle : MonoBehaviour
{
    public GameObject TV_ON_Prefab;
    public bool isOn;
    void Start ()
    {
        isOn = false;
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(isOn == false)
        {
            if (other.name == "Controller (right)" || other.name == "Controller (left)")
            {
                isOn = true;
                GameObject.Find("TV_Light").GetComponent<Light>().enabled = true;
                TV_ON_Prefab.SetActive(true);
                if (GameObject.Find("TV_OFF") != null)
                {
                    GameObject.Find("TV_OFF").SetActive(false);
                }

                //if (GameObject.Find("House_Wall_Deleted") != null)
                //{
                //    GameObject.Find("House_Wall_Deleted").SetActive(false);
                //}

                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
            }
        }
    }
}
