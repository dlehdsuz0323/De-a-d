using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//  정확한 기능을 몰라서 수정을 안했는데 LastPos 에  GameObject.Find <--- 이거 사양 ㅈㄴ 먹음 딴거 쓰는걸 추천한다
// 
//


public class SceneChanger : MonoBehaviour
{
    public GameObject RigCamera;
    public GameObject EnddingGo;
    public GameObject test1;
    public GameObject test2;

    private void OnTriggerStay(Collider collision)
    {
        if (SceneManager.GetActiveScene().name == "ReporterHouse")
        {
            if (collision.gameObject.tag == "Scene_Change")
                SceneManager.LoadScene("Mountain");
        }

        if (SceneManager.GetActiveScene().name == "Mountain")
        {
            if (collision.gameObject.tag == "Scene_Change")
                SceneManager.LoadScene("Escape House - Bustar");
        }

        if (SceneManager.GetActiveScene().name == "Escape House - Bustar")
        {
            if (collision.gameObject.tag == "Scene_Change")
                SteamVR_Fade.Start(Color.black, 2f, true);
                //StartCoroutine(LastPos());
        }
    }


    private IEnumerator LastPos()
    {
        SteamVR_Fade.Start(Color.black, 2f, true);
        yield return new WaitForSeconds(2.0f);
        Debug.Log("Game Clear");
        SteamVR_Fade.Start(Color.clear, 2f, true);

        RigCamera = GameObject.Find("[CameraRig]");
        EnddingGo = GameObject.Find("NewPaper_Ending");
        test1 = GameObject.Find("TeleportCube");
        test2 = GameObject.Find("Camera (eye)");

        RigCamera.GetComponent<CapsuleCollider>().enabled = false;
        RigCamera.GetComponent<Rigidbody>().useGravity = false;
        RigCamera.transform.position = new Vector3(10000f, 10000f, 10000f);

        test1.transform.position = test2.transform.position;

        EnddingGo.transform.parent = test2.transform;
        EnddingGo.transform.Translate(-Vector3.forward * 2);

        yield break;

    }

}
