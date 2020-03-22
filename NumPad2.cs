using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewtonVR.Example

{
	public class NumPad2 : MonoBehaviour
	{
        public NVRButton Button1;
		public NVRButton Button2;
		public NVRButton Button3;
		public NVRButton Button4;
		public NVRButton Button5;
		public NVRButton Button6;
		public NVRButton Button7;
		public NVRButton Button8;
		public NVRButton Button9;
		public NVRButton Button0;
		public NVRButton ButtonEscape;
		public NVRButton ButtonEnter;
		public NVRButton ButtonDelete;

	//	public NVRButton ButtonKeyPad;
		public GameObject HintCard;

		public string ClearNum = "87";
		public string EnterNum = null;

		public Text NumText;
		public Text MessText;

        public bool bSetActive;

		//Rigidbody rb;
	    //public static NumPad2 S;

		void Awake()
        {
            //	S = this;
            //	rb = HintCard.GetComponent<Rigidbody> ();
        }

		void EscapeSuceess()
        {
			ClearNum = ClearNum.Substring (ClearNum.Length);
			EnterNum = EnterNum.Substring (EnterNum.Length);
			MessText.text = "성공";
            //rb.isKinematic = false;
            //		Destroy (ButtonKeyPad);
            	Destroy (this.gameObject, 3f);

            // col setActive
            if (this.name == "NumberPad")
            {
                GameObject.Find("GoToSecondeRoomDoor_TeleportGuard").SetActive(false);
                GameObject.Find("EventToSecondeRoomDoor").GetComponent<DoorHingrJoint>().enabled = true;
                GameObject.Find("EventToSecondeRoomDoor").GetComponent<BoxCollider>().enabled = true;
                GameObject.Find("KeyPad").SetActive(false);
            }

            if (this.name == "NumberPad - PhotoRoom")
            {
                GameObject.Find("PhotoRoomDoor").GetComponent<BoxCollider>().enabled = true;
                GameObject.Find("KeyPad - PhotoRoom").SetActive(false);
            }


		}

		void PuzzleRe()
        {
            MessText.text = "    ";
			EnterNum = EnterNum.Substring (EnterNum.Length);
		}

		void Update()
		{
            //if (EnterNum.Length > ClearNum.Length) 
            {
			//	EnterNum = EnterNum.Substring (EnterNum.Length);
			//	NumText.text = EnterNum;
			}

			if (Button1.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "1");
				NumText.text = EnterNum;
			}
			if (Button2.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "2");
				NumText.text = EnterNum;
			}
			if (Button3.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "3");
				NumText.text = EnterNum;
			}
			if (Button4.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "4");
				NumText.text = EnterNum;
			}
			if (Button5.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "5");
				NumText.text = EnterNum;
			}
			if (Button6.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "6");
				NumText.text = EnterNum;
			}
			if (Button6.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "6");
				NumText.text = EnterNum;
			}
			if (Button7.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "7");
				NumText.text = EnterNum;
			}
			if (Button8.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "8");
				NumText.text = EnterNum;
			}
			if (Button9.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "9");
				NumText.text = EnterNum;
			}
			if (Button0.ButtonDown)
			{
				EnterNum = EnterNum.Insert (EnterNum.Length, "0");
				NumText.text = EnterNum;
			}
			if (ButtonEscape.ButtonDown)
			{
				this.gameObject.SetActive (false);
				EnterNum = EnterNum.Substring (EnterNum.Length);
				NumText.text = EnterNum;
				MessText.text = EnterNum;
                    ButtonEscape.GetComponent<NVRButton>().ButtonDown = false;
                    ButtonEscape.GetComponent<NVRButton>().ButtonIsPushed = false;
			}


			if (ButtonEnter.ButtonDown)
			{
			//	if (EnterNum.Length == ClearNum.Length)
                {
					if (ClearNum == EnterNum)
                    {
						EscapeSuceess ();
						NumText.text = EnterNum;
					}
                    else
                    {
                        MessText.text = "실패";                      
                        Invoke("PuzzleRe", 2f);
                        //PuzzleRe();
						NumText.text = null;
					}
				}

		//		if (EnterNum.Length != ClearNum.Length)
                {
					EnterNum = EnterNum.Substring (EnterNum.Length);
					NumText.text = null;
				}
			}
			if (ButtonDelete.ButtonDown)
			{
				if (EnterNum.Length > 0) {
					EnterNum = EnterNum.Substring (0, EnterNum.Length - 1);
					NumText.text = EnterNum;
				}
			}

		}
	}
}