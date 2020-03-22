using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour {
    
    private int timenum = 1;
    
	void Start ()
    {
        StartCoroutine("Example");
        Debug.Log("Coroutine OUT!");
    }

	void Update ()
    {
		
	}

    IEnumerable Example()
    {
        Debug.Log("Coroutine IN!");
    //    while (true)
        {
            Debug.Log(timenum);
            Debug.Log("초 경과");
            yield return new WaitForSeconds(1.0f);
            timenum += 1;
        }
   //     yield return null;
    }
}
