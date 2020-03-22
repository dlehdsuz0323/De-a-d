using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseIsTouch : MonoBehaviour
{
    public GameObject House_Wall_Deleted;
    private bool isOn;

    private void Awake()
    {
        isOn = false;
    }
    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (isOn == false)
        {
            if (other.name == "Controller (right)" || other.name == "Controller (left)")
            {
                isOn = true;
                House_Wall_Deleted.SetActive(false);

                 AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
            }
        }
    }
}
