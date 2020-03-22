using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TutorialControllerText : MonoBehaviour
{
    private float DelayTime;

    public Text HandText;

    [Header("Controller Tutorial Text")]
    public string[] HandTutorialText;

    public float BetweenTime;

    public void Awake()
    {
        DelayTime = 0f;

        HandText = this.GetComponent<Text>();

        if (BetweenTime == 0)
        {
            BetweenTime = 3.0f;
        }
    }

    void Start()
    {
        //b 빛나는 버튼을 눌러주세요!
        // 이 버튼을 이용하여 이동할 수 있습니다.
        // 파란색 레이저는 이동 가능한 곳입니다.
        // 빨간색 레이저는 이동이 불가능한 곳입니다.
        //b 빛나는 버튼을 눌러주세요!
        // 이 버튼을 이용하면 물건과 상호작용이 가능합니다.
        // 버튼을 이용하여 물체를 집거나 서랍, 문을 열고 닫을 수 있습니다.
        //b 서랍 앞까지 가보세요.
        // 서랍을 밀고 당겨서 열거나 닫을 수 있습니다.
        //b 문 앞까지 가보세요.
        // 문을 밀고 당겨서 열거나 닫을 수 있습니다.
        // 수고하셨습니다.
        // 포탈로 이동해 보세요!

        HandTutorialText = new string[13];
        HandTutorialText[0] = " ";
        HandTutorialText[1] = "컨트롤러의 뒷면에 위치한 \n초록색 트리거 \n버튼을 눌러주세요!";
        HandTutorialText[2] = "이 버튼을 이용하여 \n이동할 수 있습니다.";
        HandTutorialText[3] = "파란색 레이저는 이동 가능한 곳입니다.\n 빨간색 레이저는 이동이 불가능한 곳입니다.";
        HandTutorialText[4] = "컨트롤러의 양 옆에 위치한 초록색 버튼을 눌러주세요!";
        HandTutorialText[5] = "이 버튼을 이용하면 물건과 상호작용이 가능합니다.";
        HandTutorialText[6] = "버튼을 이용하여 물체를 집거나 \n 서랍, 문을 열고 닫을 수 있습니다.";
        HandTutorialText[7] = "서랍 앞까지 가보세요.";
        HandTutorialText[8] = "컨트롤러를 물체에 닿게하여서 버튼을 이용하여\n 서랍을 밀고 당겨서 열거나 닫을 수 있습니다.";
        HandTutorialText[9] = "문 앞까지 가보세요.";
        HandTutorialText[10] = "버튼을 이용하여 문을 밀고 당겨서 \n 열거나 닫을 수 있습니다";
        HandTutorialText[11] = "수고하셨습니다.";
        HandTutorialText[12] = "뒷쪽의 포탈로 이동해 보세요!";

        StartCoroutine(Tutorial1());
    }

    void Update()
    {

    }

    IEnumerator Tutorial1()
    {
        if (!GameSupervisor.Instance.isHandTutorial[0])
        {
            HandText.text = this.HandTutorialText[1];

            if (GameSupervisor.Instance.isRightHand)
            {
                //if(!GameSupervisor.Instance.isLeftHand)
                //{
                //    GameSupervisor.Instance.LeftHand.transform.parent.gameObject.SetActive(false);
                //}

                GameSupervisor.Instance.LeftHand.transform.parent.gameObject.SetActive(false);
                GameSupervisor.Instance.ChangeController(2, true);
            }
            else if (GameSupervisor.Instance.isLeftHand)
            {
                //if (!GameSupervisor.Instance.isRightHand)
                //{
                //    GameSupervisor.Instance.RightHand.transform.parent.gameObject.SetActive(false);
                //}

                GameSupervisor.Instance.RightHand.transform.parent.gameObject.SetActive(false);
                GameSupervisor.Instance.ChangeController(2, true);
            }
            else
            {
                while (!GameSupervisor.Instance.isRightHand && !GameSupervisor.Instance.isLeftHand)
                {
                    //DelayTime += Time.deltaTime;
                    //if(DelayTime >= 5.0f)
                    //{
                    //    yield break;
                    //}
                    yield return null;
                }
                yield return StartCoroutine(Tutorial1());
            }

            while (!GameSupervisor.Instance.isTrigger)
            {
                yield return null;
            }

            if (GameSupervisor.Instance.isTrigger)
            {
                //this.transform.GetComponentInParent<AudioSource>().PlayOneShot(TutorialAudioClip);
                //PickObject.Instance.OnLager();
                GameSupervisor.Instance.ChangeController(2, false);

                HandText.text = this.HandTutorialText[2];
                yield return new WaitForSeconds(BetweenTime);
                HandText.text = this.HandTutorialText[3];
                yield return new WaitForSeconds(BetweenTime);
                GameSupervisor.Instance.OnLager();
                GameSupervisor.Instance.isHandTutorial[0] = true;
                yield return StartCoroutine(Tutorial2());
            }
        }
        else
        {
            yield return StartCoroutine(Tutorial2());
        }
    }
    IEnumerator Tutorial2()
    {
        if (!GameSupervisor.Instance.isHandTutorial[1])
        {
            HandText.text = this.HandTutorialText[4];
            GameSupervisor.Instance.ChangeController(3, true);
            GameSupervisor.Instance.ChangeController(4, true);

            while (!GameSupervisor.Instance.isGrip)
            {
                yield return null;
            }
            if (GameSupervisor.Instance.isGrip)
            {
                //this.transform.GetComponentInParent<AudioSource>().PlayOneShot(TutorialAudioClip);

                GameSupervisor.Instance.ChangeController(3, false);
                GameSupervisor.Instance.ChangeController(4, false);

                HandText.text = this.HandTutorialText[5];
                yield return new WaitForSeconds(BetweenTime);
                HandText.text = this.HandTutorialText[6];
                yield return new WaitForSeconds(BetweenTime);
                GameSupervisor.Instance.isHandTutorial[1] = true;
                yield return StartCoroutine(Tutorial3());
            }
        }
        else
        {
            yield return StartCoroutine(Tutorial3());
        }
    }
    IEnumerator Tutorial3()
    {
        if (!GameSupervisor.Instance.isHandTutorial[2])
        {
            HandText.text = this.HandTutorialText[7];
            GameSupervisor.Instance.TutorialChangeMaterial(GameSupervisor.Instance.Drawer, GameSupervisor.Instance.MatTutorial);

            while (!GameSupervisor.Instance.isDrawer[0])
            {
                yield return null;
            }
            HandText.text = this.HandTutorialText[8];
            while (!GameSupervisor.Instance.isDrawer[1])
            {
                yield return null;
            }
            if (GameSupervisor.Instance.isDrawer[1])
            {
                GameSupervisor.Instance.isHandTutorial[2] = true;
                GameSupervisor.Instance.TutorialChangeMaterial(GameSupervisor.Instance.Drawer, GameSupervisor.Instance.MatPrevious);
                yield return StartCoroutine(Tutorial4());
            }
        }
        else
        {
            yield return StartCoroutine(Tutorial4());
        }
    }
    IEnumerator Tutorial4()
    {
        if (!GameSupervisor.Instance.isHandTutorial[3])
        {
            HandText.text = this.HandTutorialText[9];
            GameSupervisor.Instance.TutorialChangeMaterial(GameSupervisor.Instance.Door, GameSupervisor.Instance.MatTutorial);

            while (!GameSupervisor.Instance.isDoor[0])
            {
                yield return null;
            }
            HandText.text = this.HandTutorialText[10];
            while (!GameSupervisor.Instance.isDoor[1])
            {
                yield return null;
            }
            if (GameSupervisor.Instance.isDoor[1])
            {
                HandText.text = this.HandTutorialText[11];
                GameSupervisor.Instance.TutorialChangeMaterial(GameSupervisor.Instance.Door, GameSupervisor.Instance.MatPrevious);
                yield return new WaitForSeconds(BetweenTime);
                HandText.text = this.HandTutorialText[12];
                GameSupervisor.Instance.TutorialChangeMaterial(GameSupervisor.Instance.Potal, GameSupervisor.Instance.MatTutorial);
                GameSupervisor.Instance.Potal.GetComponent<SceneChange>().enabled = true;
                yield return new WaitForSeconds(BetweenTime);
                GameSupervisor.Instance.isHandTutorial[3] = true;
                //HandText.text = this.HandTutorialText[0];
                yield break;
            }
        }
        else
        {
            //HandText.text = this.HandTutorialText[11];
            //yield return new WaitForSeconds(1.0f);
            //HandText.text = this.HandTutorialText[12];
        }
    }


}
