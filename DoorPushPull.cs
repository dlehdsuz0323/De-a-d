using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental;

public class DoorPushPull : MonoBehaviour
{
    [Range(-180f, 180f)]
    public float Angle;
    [Range(0.05f, 0.2f)]
    public float Smooth;

    private GameObject TargetObject;

    private Quaternion MinRotate;
    private Quaternion MaxRotate;

    private Vector3 MousePreviousPosition;
    private Vector3 MouseCurrentPosition;

    private bool isClicked;

    private void Awake()
    {
        if (Angle == 0)
            Angle = -110f;
        if (Smooth == 0)
            Smooth = 0.05f;

        MinRotate = this.transform.rotation;
        MaxRotate = Quaternion.Euler(0,Angle,0);

        isClicked = false;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if(isClicked)
        {
            if (GameSupervisor.Instance.isGrip)
            {
                MousePreviousPosition = MouseCurrentPosition;

                MouseCurrentPosition = TargetObject.transform.position;
                MouseCurrentPosition.y = 0;

                if (MousePreviousPosition.z < MouseCurrentPosition.z)
                {
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, MaxRotate, Smooth);
                }
                else if (MousePreviousPosition.z > MouseCurrentPosition.z)
                {
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, MinRotate, Smooth);
                }
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
        if (!GameSupervisor.Instance.isUse && GameSupervisor.Instance.isGrip)
        {
            if ((other.name == "Controller (left)" || other.name == "Controller (right)"))
            {
                isClicked = true;
                GameSupervisor.Instance.isUse = true;
                TargetObject = other.transform.gameObject;

                if (GameSupervisor.Instance.isTutorial)
                {
                    if (GameSupervisor.Instance.isDoor[0])
                        GameSupervisor.Instance.isDoor[1] = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
    }
}
