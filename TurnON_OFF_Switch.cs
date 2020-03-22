using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnON_OFF_Switch : MonoBehaviour {

    public int ButtonON;
    public int ButtonOFF;
    Quaternion ChangeRotation;

    private void Awake()
    {
        ButtonOFF = -60;
        ButtonON = 60;
    }

    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Controller")
        {
            Debug.Log("CRUSH!!");
            Debug.Log(gameObject.transform.rotation);
        }


        if (-0.5f == gameObject.transform.rotation.x)
        {
            gameObject.transform.rotation = Quaternion.Euler(ButtonON, 0, 0);
            GameObject.Find("EventToHintNumberLight").GetComponent<Light>().enabled = true;
            GameObject.Find("EventToHintNumberCanvas").GetComponent<Canvas>().enabled = true;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();


        }

        //else if(0.5f == gameObject.transform.rotation.x)
        //{
        //    gameObject.transform.rotation = Quaternion.Euler(ButtonOFF, 0, 0);
        //    GameObject.Find("EventToHintNumberLight").GetComponent<Light>().enabled = false;
        //    GameObject.Find("EventToHintNumberCanvas").GetComponent<Canvas>().enabled = false;
        //}
    }

}
