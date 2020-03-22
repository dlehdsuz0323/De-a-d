using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerPushPull : MonoBehaviour
{
    [Range(0.1f, 1f)]
    public float Smooth;
    [Range(0, 0.7f)]
    public float PullMax;

    public GameObject TargetObject;

    private Vector3 MinPos;
    private Vector3 MaxPos;
    private Vector3 MousePreviousPosition;
    private Vector3 MouseCurrentPosition;

    private float Dist;

    private bool isClicked;

    private void Awake()
    {
        if (Smooth == 0)
            Smooth = 0.8f;
        if (PullMax == 0)
            PullMax = 0.7f;

        MinPos = this.transform.position;
        MaxPos = this.transform.position - this.transform.forward * PullMax;

        isClicked = false;
    }

    private void Start()
    {

    }

    private void Update()
    {
        #region aa
        //if (Input.GetMouseButtonDown(0))
        //{
        //    isClicked = true;
        //    MouseCurrentPosition = Input.mousePosition;
        //    MouseCurrentPosition.z = 0;
        //    MousePreviousPosition = MouseCurrentPosition;
        //}
        //if (Input.GetMouseButton(0) && isClicked)
        //{
        //    MousePreviousPosition = MouseCurrentPosition;
        //    MouseCurrentPosition = Input.mousePosition;
        //    MouseCurrentPosition.z = 0;

        //    Dist = Vector3.Distance(MouseCurrentPosition, MousePreviousPosition);
        //    Dist /= Smooth;

        //    if (MousePreviousPosition.y < MouseCurrentPosition.y)
        //    {
        //        this.transform.Translate(Vector3.forward * Time.deltaTime * Dist);
        //    }
        //    else if (MousePreviousPosition.y > MouseCurrentPosition.y)
        //    {
        //        this.transform.Translate(-Vector3.forward * Time.deltaTime * Dist);
        //    }

        //    if (MousePreviousPosition.y < MouseCurrentPosition.y)
        //    {
        //        this.transform.Translate(Vector3.forward * Time.deltaTime * Dist);

        //    }
        //    else if (MousePreviousPosition.y > MouseCurrentPosition.y)
        //    {
        //        this.transform.Translate(-Vector3.forward * Time.deltaTime * Dist);
        //    }

        //    if (this.transform.eulerAngles.y == 0)
        //    {
        //        if (this.transform.position.z > MinPos.z)
        //            this.transform.position = MinPos;
        //        if (this.transform.position.z < MaxPos.z)
        //            this.transform.position = MaxPos;
        //    }
        //    if (this.transform.eulerAngles.y == 180)
        //    {
        //        if (this.transform.position.z < MinPos.z)
        //            this.transform.position = MinPos;
        //        if (this.transform.position.z > MaxPos.z)
        //            this.transform.position = MaxPos;
        //    }
        //    if (this.transform.eulerAngles.y < 360 & this.transform.eulerAngles.y > 180)
        //    {
        //        if (this.transform.position.x < MinPos.x)
        //            this.transform.position = MinPos;
        //        if (this.transform.position.x > MaxPos.x)
        //            this.transform.position = MaxPos;
        //    }
        //    if (this.transform.eulerAngles.y > 0 & this.transform.eulerAngles.y < 180)
        //    {
        //        if (this.transform.position.x > MinPos.x)
        //            this.transform.position = MinPos;
        //        if (this.transform.position.x < MaxPos.x)
        //            this.transform.position = MaxPos;
        //    }
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    isClicked = false;
        //}
        #endregion
        if (isClicked)
        {
            if (GameSupervisor.Instance.isGrip)
            {
                MousePreviousPosition = MouseCurrentPosition;
                MouseCurrentPosition = TargetObject.transform.position;
                MouseCurrentPosition.x = 0;
                MouseCurrentPosition.y = 0;

                Dist = Vector3.Distance(MouseCurrentPosition, MousePreviousPosition);
                Dist /= Smooth;

                if (MousePreviousPosition.z < MouseCurrentPosition.z)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * Smooth);
                }
                else if (MousePreviousPosition.z > MouseCurrentPosition.z)
                {
                    this.transform.Translate(-Vector3.forward * Time.deltaTime * Smooth);
                }

                #region CheckPosition
                if (MousePreviousPosition.z < MouseCurrentPosition.z)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * Dist);

                }
                else if (MousePreviousPosition.z > MouseCurrentPosition.z)
                {
                    this.transform.Translate(-Vector3.forward * Time.deltaTime * Dist);
                }

                if (this.transform.eulerAngles.y == 0)
                {
                    if (this.transform.position.z > MinPos.z)
                        this.transform.position = MinPos;
                    if (this.transform.position.z < MaxPos.z)
                        this.transform.position = MaxPos;
                }
                if (this.transform.eulerAngles.y == 180)
                {
                    if (this.transform.position.z < MinPos.z)
                        this.transform.position = MinPos;
                    if (this.transform.position.z > MaxPos.z)
                        this.transform.position = MaxPos;
                }
                if (this.transform.eulerAngles.y < 360 & this.transform.eulerAngles.y > 180)
                {
                    if (this.transform.position.x < MinPos.x)
                        this.transform.position = MinPos;
                    if (this.transform.position.x > MaxPos.x)
                        this.transform.position = MaxPos;
                }
                if (this.transform.eulerAngles.y > 0 & this.transform.eulerAngles.y < 180)
                {
                    if (this.transform.position.x > MinPos.x)
                        this.transform.position = MinPos;
                    if (this.transform.position.x < MaxPos.x)
                        this.transform.position = MaxPos;
                }
                #endregion
            }
            else
            {
                GameSupervisor.Instance.isUse = false;
                isClicked = false;
                TargetObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
    private void OnTriggerStay(Collider other)
    {
        if(!GameSupervisor.Instance.isUse && GameSupervisor.Instance.isGrip)
        {
            if ((other.name == "Controller (left)" || other.name == "Controller (right)"))
            {
                isClicked = true;
                GameSupervisor.Instance.isUse = true;
                TargetObject = other.transform.gameObject;

                if (GameSupervisor.Instance.isTutorial)
                {
                    if (GameSupervisor.Instance.isDrawer[0])
                        GameSupervisor.Instance.isDrawer[1] = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
    }
}
