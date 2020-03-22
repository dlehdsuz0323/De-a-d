using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramePuzzle : MonoBehaviour
{
    public static FramePuzzle S;
    public string FrameClear = "1234";
    public string FrameNum = null;


    void CheckFrameNum()
    {
        if (FrameNum.Length > FrameClear.Length)
        {
            FrameNum = FrameNum.Substring(0, FrameNum.Length - 1);
            GameObject.Find("FirstFrame_Light").    GetComponent<Light>().enabled = false;
            GameObject.Find("SecondeFrame_Light").  GetComponent<Light>().enabled = false;
            GameObject.Find("ThirdFrame_Light").    GetComponent<Light>().enabled = false;
            GameObject.Find("LastFrame_Light").     GetComponent<Light>().enabled = false;
        }
    }

    void ClearCheck()
    {
        if (FrameNum.Length == FrameClear.Length)
        {
            if (FrameNum == FrameClear)
            {
                FrameClear = FrameClear.Substring(FrameClear.Length);
                FrameNum = FrameNum.Substring(FrameNum.Length);
                Debug.Log("FREAM PUZZLE CLEAR");

                GameObject go = GameObject.Find("FramePuzzle_chest_of_drawers_broken");
                Destroy(go);
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();

            }
            else
            {
                FrameNum = FrameNum.Substring(FrameNum.Length);
                Debug.Log("FREAM PUZZLE RESET");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject coll = other.gameObject;
        switch (coll.name)
        {
            case "FirstFrame":
                FrameNum = FrameNum.Insert(FrameNum.Length, "1");
                GameObject.Find("FirstFrame_Light").GetComponent<Light>().enabled = true;


                CheckFrameNum();
                ClearCheck();
                break;
            case "SecondeFrame":
                FrameNum = FrameNum.Insert(FrameNum.Length, "2");
                GameObject.Find("SecondeFrame_Light").GetComponent<Light>().enabled = true;

                CheckFrameNum();
                ClearCheck();
                break;

            case "ThirdFrame":
                FrameNum = FrameNum.Insert(FrameNum.Length, "3");
                GameObject.Find("ThirdFrame_Light").GetComponent<Light>().enabled = true;

                CheckFrameNum();
                ClearCheck();
                break;

            case "LastFrame":
                FrameNum = FrameNum.Insert(FrameNum.Length, "4");
                GameObject.Find("LastFrame_Light").GetComponent<Light>().enabled = true;

                CheckFrameNum();
                ClearCheck();
                break;
        }
    }
    //void OnCollisionEnter(Collision coll)
    //{


    //    GameObject other = coll.gameObject;
    //    switch (other.name)
    //    {
    //        case "FirstFrame":
    //            FrameNum = FrameNum.Insert(FrameNum.Length, "1");
    //            GameObject.Find("FirstFrame_Light").GetComponent<Light>().enabled = true;
                

    //            CheckFrameNum();
    //            ClearCheck();
    //            break;
    //        case "SecondeFrame":
    //            FrameNum = FrameNum.Insert(FrameNum.Length, "2");
    //            GameObject.Find("SecondeFrame_Light").GetComponent<Light>().enabled = true;

    //            CheckFrameNum();
    //            ClearCheck();
    //            break;

    //        case "ThirdFrame":
    //            FrameNum = FrameNum.Insert(FrameNum.Length, "3");
    //            GameObject.Find("ThirdFrame_Light").GetComponent<Light>().enabled = true;

    //            CheckFrameNum();
    //            ClearCheck();
    //            break;

    //        case "LastFrame":
    //            FrameNum = FrameNum.Insert(FrameNum.Length, "4");
    //            GameObject.Find("LastFrame_Light").GetComponent<Light>().enabled = true;

    //            CheckFrameNum();
    //            ClearCheck();
    //            break;
    //    }
    //}

}
