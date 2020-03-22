using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPuzzle : MonoBehaviour
{
    public GameObject Target;
    public bool isOn;

    private void Awake()
    {
        isOn = true;
    }
    void Start ()
    {
		
	}
		
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (isOn == true)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            isOn = false;
        }
        Target.SetActive(true);
    }
}
