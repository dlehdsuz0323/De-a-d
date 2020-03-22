using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public float animTime = 2f;

    private Image fadeImage;

    private float start = 1f;
    private float end = 0f;
    private float time = 0f;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayFadeOut();	
	}

    void PlayFadeOut()
    {
        time += Time.deltaTime / animTime;

        Color color = fadeImage.color;
        color.a = Mathf.Lerp(start, end, time);
        fadeImage.color = color;
    }
}
