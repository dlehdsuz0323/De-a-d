using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderLight : MonoBehaviour
{
   public int BlickOfNum = 5;
   private bool Benabled = true;
    private bool BCoroutine = false;
    private int timenum = 1;

    void Start ()
    {
        StartCoroutine("BlinkThunderLight");    
    }
	
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Light false");
            gameObject.GetComponent<Light>().enabled = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Light false");
            gameObject.GetComponent<Light>().enabled = true;
        }

        

    }


    private IEnumerator BlinkThunderLight()
    {
        while (true)
        {
            if (BCoroutine == false)
            {
                yield return new WaitForSeconds(3.0f);
                Debug.Log("BCoroutine = false");
                BCoroutine = true;
            }
            else if (BCoroutine == true)
            {
                yield return new WaitForSeconds(27.0f);
                Debug.Log("BCoroutine = true");
                BCoroutine = false;
            }

            int countTime = 0;
            while (countTime < 6)
            {
                if (countTime % 2 == 0)
                {
                    gameObject.GetComponent<Light>().enabled = false;
                }
                else
                {
                    gameObject.GetComponent<Light>().enabled = true;
                }
                yield return new WaitForSeconds(0.05f);
                countTime++;
            }


            gameObject.GetComponent<Light>().enabled = false;
            yield return null;

        }

        //Debug.Log("Coroutine IN!");
        //    while (true)
        //{
        //    Debug.Log(timenum);
        //    Debug.Log("초 경과");
        //    yield return new WaitForSeconds(1.0f);
        //    timenum += 1;
        //}
    }
}

