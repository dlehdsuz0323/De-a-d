using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSoundLoop : MonoBehaviour {

    public static RainSoundLoop instance;
    public AudioClip RainSound;
    public float delaytime;

    private void Awake()
    {
        RainSoundLoop.instance = this;
        delaytime = 5.0f;
    }

    // Use this for initialization
    void Start ()
    {
        StartCoroutine("SoundLoop");
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

}
