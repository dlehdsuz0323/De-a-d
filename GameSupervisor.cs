using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSupervisor : MonoBehaviour
{
    #region Singleton
    public static GameSupervisor Instance;
    #endregion

    #region VRController
    private int RightIndex;
    private int LeftIndex;
    public GameObject RightHand;
    public GameObject LeftHand;
    public SteamVR_Controller.Device RightDevice;
    public SteamVR_Controller.Device LeftDevice;
    public bool isRightHand;
    public bool isLeftHand;

    public List<GameObject> RightDevices;
    public List<GameObject> LeftDevices;

    public Material OpacityMaterial;
    private Color HightLightColor;

    [Header("ControllerButtonInput")]
    public bool isUse;
    public bool isTrigger;
    public bool isGrip;
    public bool isTouchPad;
    #endregion
    
    #region TutorialBool
    public bool isTutorial;
    public bool[] isHandTutorial;
    public bool[] isMainTutorial;

    public bool[] isDrawer;
    public bool[] isDoor;

    #endregion

    #region TutorialText
    [Header("Main Tutorial Text")]
    public string[] MainTutorialText;
    #endregion

    #region TutorialObject
    public GameObject Door;
    public GameObject Drawer;
    public GameObject Potal;
    #endregion

    #region TutorialMat
    public Material MatPrevious;
    public Material MatTutorial;
    #endregion

    private void Awake()
    {
        #region Singleton
        if (GameSupervisor.Instance == null)
            Instance = this;
        #endregion

        #region VRController
        isTrigger = false;
        isGrip = false;
        isTouchPad = false;
        //OpacityColor = new Color32(30, 30, 30, 80);
        HightLightColor = new Color32(0, 255, 0, 255);
        #endregion

        #region TutorialBool
        isTutorial = true;
        isHandTutorial = new bool[5];
        isMainTutorial = new bool[5];
        isDrawer = new bool[2];
        isDoor = new bool[2];
        #endregion

        #region TurorialText


        MainTutorialText = new string[10];
        MainTutorialText[0] = " ";
        MainTutorialText[1] = "Tutorial";
        MainTutorialText[2] = "StartGame";
        MainTutorialText[3] = "1";
        MainTutorialText[4] = "2";
        MainTutorialText[5] = "3";
        MainTutorialText[6] = "4";
        MainTutorialText[7] = "4";
        MainTutorialText[8] = "End!";
        #endregion

        #region TutorialObject
        Door = GameObject.Find("Door");
        Drawer = GameObject.Find("Drawer");
        Potal = GameObject.Find("Potal");
        #endregion
    }

    private void Start()
    {
        #region VRController
        StartCoroutine(ControllerInit());
        #endregion
    }

    private void Update()
    {
        #region VRControllerInput
        #region Opacity
        ControllerOpacity();
        #endregion
        DeviceButtonState();
    }

    private IEnumerator ControllerInit()
    {
        RightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        if (RightIndex == -1)
            yield return StartCoroutine(ControllerInit());
        LeftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        if (RightIndex == -1)
            yield return StartCoroutine(ControllerInit());
        RightDevice = SteamVR_Controller.Input(RightIndex);
        LeftDevice = SteamVR_Controller.Input(LeftIndex);

        if (RightDevice == null || LeftDevice == null)
            yield return StartCoroutine(ControllerInit());
        else
            yield break;
    }

    private void ControllerOpacity()
    {
        //if (RightHand == null)
        //{
        //    RightHand = GameObject.Find("Controller (right)").transform.Find("Model").gameObject;
        //    if (RightHand == null)
        //        return;
        //}
        //if (LeftHand == null)
        //{
        //    LeftHand = GameObject.Find("Controller (left)").transform.Find("Model").gameObject;
        //    if (LeftHand == null)
        //        return;
        //}
        if (RightDevice != null && RightHand != null && !isRightHand)
        {
            if (RightHand.transform.childCount == 0)
                return;
            RightDevices.Add(RightHand.transform.Find("body").gameObject);
            RightDevices.Add(RightHand.transform.Find("trackpad").gameObject);
            RightDevices.Add(RightHand.transform.Find("trigger").gameObject);
            RightDevices.Add(RightHand.transform.Find("lgrip").gameObject);
            RightDevices.Add(RightHand.transform.Find("rgrip").gameObject);
            RightDevices.Add(RightHand.transform.Find("button").gameObject);
            RightDevices.Add(RightHand.transform.Find("sys_button").gameObject);

            for (int i = 0; i < RightDevices.Count; i++)
            {
                RightDevices[i].GetComponent<MeshRenderer>().material = OpacityMaterial;
            }
            isRightHand = true;
        }
        if (LeftDevice != null && LeftHand != null && !isLeftHand)
        {
            if (LeftHand.transform.childCount == 0)
                return;
            LeftDevices.Add(LeftHand.transform.Find("body").gameObject);
            LeftDevices.Add(LeftHand.transform.Find("trackpad").gameObject);
            LeftDevices.Add(LeftHand.transform.Find("trigger").gameObject);
            LeftDevices.Add(LeftHand.transform.Find("lgrip").gameObject);
            LeftDevices.Add(LeftHand.transform.Find("rgrip").gameObject);
            LeftDevices.Add(LeftHand.transform.Find("button").gameObject);
            LeftDevices.Add(LeftHand.transform.Find("sys_button").gameObject);
            for (int i = 0; i < LeftDevices.Count; i++)
            {
                LeftDevices[i].GetComponent<MeshRenderer>().material = OpacityMaterial;
            }
            isLeftHand = true;
        }
    }

    public void ChangeController(int controllerIndex, bool opa)
    {
        // 1 : trackpad, 2 : trigger, 3,4 : grip 
        // true : hightlight            flase : opacity

        if (isRightHand)
        {
            if (opa)
            {
                if (RightDevices[controllerIndex] != null)
                    RightDevices[controllerIndex].GetComponent<MeshRenderer>().material.color = HightLightColor;
            }
            else
            {
                if (RightDevices[controllerIndex] != null)
                    RightDevices[controllerIndex].GetComponent<MeshRenderer>().material.color = OpacityMaterial.color;
            }
        }
        if (isLeftHand)
        {
            if (opa)
            {
                if (LeftDevices[controllerIndex] != null)
                    LeftDevices[controllerIndex].GetComponent<MeshRenderer>().material.color = HightLightColor;
            }
            else
            {
                if (LeftDevices[controllerIndex] != null)
                    LeftDevices[controllerIndex].GetComponent<MeshRenderer>().material.color = OpacityMaterial.color;
            }
        }


    }

    public void TutorialChangeMaterial(GameObject go, Material mat)
    {
        if (go.GetComponent<MeshRenderer>() != null)
        {
            go.GetComponent<MeshRenderer>().material = mat;
        }

        for (int i = 0; i < go.transform.childCount; i++)
        {
            if (go.transform.GetChild(i).GetComponent<MeshRenderer>() != null)
            {
                go.transform.GetChild(i).GetComponent<MeshRenderer>().material = mat;
            }
        }
    }

    public void OnLager()
    {
        if(RightHand.transform.parent.GetComponent<LaserPointer>() != null)
        {
            RightHand.transform.parent.GetComponent<LaserPointer>().enabled = true;
        }
        if (LeftHand.transform.parent.GetComponent<LaserPointer>() != null)
        {
            LeftHand.transform.parent.GetComponent<LaserPointer>().enabled = true;
        }
    }
    public void DeviceButtonState()
    {
        if (RightDevice == null && LeftDevice == null)
            return;
        #region Down
        if (RightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) ||
            LeftDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            GameSupervisor.Instance.isTouchPad = true;
        }
        if (RightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) ||
            LeftDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameSupervisor.Instance.isTrigger = true;
        }
        if (RightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip) ||
            LeftDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            GameSupervisor.Instance.isGrip = true;
        }
        #endregion
        #region Up
        if (RightDevice.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) ||
            LeftDevice.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            GameSupervisor.Instance.isTouchPad = false;
        }
        if (RightDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) ||
            LeftDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameSupervisor.Instance.isTrigger = false;
        }
        if (RightDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip) ||
            LeftDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            GameSupervisor.Instance.isGrip = false;
        }
        #endregion
        #endregion
    }
}
