using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 쓸대없는 변수 개많누 ? bool하고 쓸대없이 사양없는 time.delta 타임 지우고 코루틴으로 1초마다 호출되게 바껏누 
// 
// 
public class Timer : MonoBehaviour
{
    [Header("게임 플레이 시간")]
    public int boomTime_sec;
    public TextMesh TimeCount;


    void Awake()
    {
        boomTime_sec = 60 * 5;
        StartCoroutine("StartTimer");
    }


    void FixedUpdate()
    {
        int min = boomTime_sec / 60;
        int sec = boomTime_sec % 60;

        if (boomTime_sec <= 0)
        {
            SteamVR_Fade.Start(Color.black, 5, true);
            print("TimeOver");
        }
        TimeCount.text = min + " : " + sec;
    }


    IEnumerator StartTimer()
    {
        while (boomTime_sec >= 0)
        {
            yield return new WaitForSeconds(1);
            boomTime_sec--;
        }
    }
}