using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR.Example
{

	public class KeyPad : MonoBehaviour {

		public NVRButton button;
		public GameObject NumPad;
        public bool isOn;

        private void Awake()
        {
            isOn = true;
        }
        public void Update()
        {
			//if (button.ButtonDown)
            //{
            //    Debug.Log("INPUT!!");
			//	  NumPad.SetActive (true);
			//}
		}

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("Input");
            if (other.name == "Controller (right)" || other.name == "Controller (left)")
            {
                if(isOn == true)
                {
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                    isOn = false;
                }
                NumPad.SetActive(true);
            }
        }


    }
}
