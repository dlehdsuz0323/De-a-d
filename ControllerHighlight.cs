using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Buttons
{
    // 0 : body, 1 : trackpad, 2 : trigger, 3 : lgrip, 4 : rgrip, 5 : menuButton, 6 : sys_button
    body, trackpad, trigger, lgrip, rgrip, menuButton, sys_button,
}

public class ControllerHighlight : MonoBehaviour
{
    public SteamVR_Controller.Device Controller;
    public List<GameObject> ControllerButtons;
    private Material OriginMaterial;
    public Material OpacityMaterial;

    private Color OriginColor;
    private Color HighlightColor;

    public GameObject GoTutorialText;

    public bool Init = false;

    private bool[] isTutorialAll;
    private bool[] isTutorial1;
    private bool[] isTutorial2;
    private bool[] isTutorial3;


    public AudioClip TutorialAudioClip;

    private void Awake()
    {
        HighlightColor = new Color(250, 250, 250, 255);
        isTutorialAll = new bool[] { false, false, false };
        isTutorial1 = new bool[2];
        isTutorial2 = new bool[4];
        isTutorial3 = new bool[2];
    }

    void Start()
    {
    }

    void Update()
    {
        StartCoroutine(InitController());
        if (!Init)
            return;

        InputButton();
        OutputButton();

        StartCoroutine(Tutorial());

    }

    void InputButton()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            GameSupervisor.Instance.isTouchPad = true;
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameSupervisor.Instance.isTrigger = true;
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            GameSupervisor.Instance.isGrip = true;
        }
    }
    void OutputButton()
    {
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            GameSupervisor.Instance.isTouchPad = false;
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameSupervisor.Instance.isTrigger = false;
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            GameSupervisor.Instance.isGrip = false;
        }
    }

    IEnumerator InitController()
    {
        while (!Init)
        {
            if (Controller == null)
            {
                Controller = SteamVR_Controller.Input((int)this.transform.parent.GetComponent<SteamVR_TrackedObject>().index);
            }

            if (Controller != null)
            {
                ControllerButtons.Add(this.transform.Find("body").gameObject);
                ControllerButtons.Add(this.transform.Find("trackpad").gameObject);
                ControllerButtons.Add(this.transform.Find("trigger").gameObject);
                ControllerButtons.Add(this.transform.Find("lgrip").gameObject);
                ControllerButtons.Add(this.transform.Find("rgrip").gameObject);
                ControllerButtons.Add(this.transform.Find("button").gameObject);
                ControllerButtons.Add(this.transform.Find("sys_button").gameObject);

                OriginMaterial = ControllerButtons[0].GetComponent<MeshRenderer>().material;

                for (int i = 0; i < ControllerButtons.Count; i++)
                {
                    ControllerButtons[i].GetComponent<MeshRenderer>().material = OpacityMaterial;
                }

                GoTutorialText = this.transform.parent.transform.Find("Canvas").transform.GetChild(0).gameObject;

                Init = true;
                StopCoroutine(InitController());
            }
            yield return null;
        }
    }

    IEnumerator Tutorial()
    {
        #region Tutorial1
        if (!isTutorialAll[0])
        {
            if (!isTutorial1[0])
            {
                GoTutorialText.GetComponent<Text>().text = "trigger";
                ControllerButtons[2].GetComponent<MeshRenderer>().material.color = HighlightColor;
                isTutorial1[0] = true;
            }

            if (!isTutorial1[1] && GameSupervisor.Instance.isTrigger)
            {
                isTutorial1[1] = true;
                this.transform.GetComponentInParent<AudioSource>().PlayOneShot(TutorialAudioClip);
                GoTutorialText.GetComponent<Text>().text = "Success!!!";
                ControllerButtons[2].GetComponent<MeshRenderer>().material.color = OpacityMaterial.color;
                yield return new WaitForSeconds(2.0f);
                isTutorialAll[0] = true;
                PickObject.Instance.OnLager();
            }
        }
        #endregion

        #region Tutorial2
        if (isTutorialAll[0] && !isTutorialAll[1])
        {
            if (!isTutorial2[3] && !GameSupervisor.Instance.isTrigger)
            {
                isTutorial2[0] = false;
                isTutorial2[1] = false;
                isTutorial2[2] = false;
                ControllerButtons[1].GetComponent<MeshRenderer>().material.color = OpacityMaterial.color;
            }

            if (!isTutorial2[0])
            {
                GoTutorialText.GetComponent<Text>().text = "trigger";
                ControllerButtons[2].GetComponent<MeshRenderer>().material.color = HighlightColor;
                isTutorial2[0] = true;
            }

            if (!isTutorial2[1] && GameSupervisor.Instance.isTrigger)
            {
                this.transform.GetComponentInParent<AudioSource>().PlayOneShot(TutorialAudioClip);
                GoTutorialText.GetComponent<Text>().text = "trackpad";
                ControllerButtons[2].GetComponent<MeshRenderer>().material.color = OpacityMaterial.color;
                ControllerButtons[1].GetComponent<MeshRenderer>().material.color = HighlightColor;
                isTutorial2[1] = true;
            }

            if (isTutorial2[1])
            {
                if (!isTutorial2[2] && GameSupervisor.Instance.isTouchPad)
                {
                    isTutorial2[2] = true;
                    this.transform.GetComponentInParent<AudioSource>().PlayOneShot(TutorialAudioClip);
                    ControllerButtons[1].GetComponent<MeshRenderer>().material.color = OpacityMaterial.color;
                }

                if(isTutorial2[2] && !GameSupervisor.Instance.isTouchPad)
                {
                    if(PickObject.Instance.isMoved)
                    {
                        isTutorial2[3] = true;
                        GoTutorialText.GetComponent<Text>().text = "Success!!!";
                        yield return new WaitForSeconds(2.0f);
                        isTutorialAll[1] = true;
                    }
                }
            }
        }
        #endregion

        #region  Tutorial3
        if (isTutorialAll[0] && isTutorialAll[1] && !isTutorialAll[2])
        {
            if (!isTutorial3[0])
            {
                GoTutorialText.GetComponent<Text>().text = "Grip";
                ControllerButtons[3].GetComponent<MeshRenderer>().material.color = HighlightColor;
                ControllerButtons[4].GetComponent<MeshRenderer>().material.color = HighlightColor;
                isTutorial3[0] = true;
            }
            if (!isTutorial3[1] && GameSupervisor.Instance.isGrip)
            {
                isTutorial3[1] = true;
                this.transform.GetComponentInParent<AudioSource>().PlayOneShot(TutorialAudioClip);
                GoTutorialText.GetComponent<Text>().text = "Success!!!";
                ControllerButtons[3].GetComponent<MeshRenderer>().material.color = OpacityMaterial.color;
                ControllerButtons[4].GetComponent<MeshRenderer>().material.color = OpacityMaterial.color;
                yield return new WaitForSeconds(2.0f);
                isTutorialAll[2] = true;
                StopCoroutine(Tutorial());
            }
        }
        #endregion

        yield return null;
    }

}
