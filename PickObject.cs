using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    public static PickObject Instance;

    public bool isMoved = false;
    private GameObject PreviousParent;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {

    }
    private void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickObject" && GameSupervisor.Instance.isGrip)
        {
            other.transform.GetComponent<Rigidbody>().isKinematic = true;
            PreviousParent = other.transform.parent.gameObject;
            other.transform.parent = this.transform;
        }

        if (other.tag == "MovePos")
        {
            isMoved = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PickObject" && GameSupervisor.Instance.isGrip)
        {
            other.gameObject.transform.position = this.transform.position;
        }
        if (other.tag == "PickObject" && !GameSupervisor.Instance.isGrip)
        {
          //  other.transform.GetComponent<Rigidbody>().isKinematic = false;    ?????
            other.transform.parent = PreviousParent.transform;
            //other.transform.GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.Impulse);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickObject")
        {
        //    other.transform.GetComponent<Rigidbody>().isKinematic = false;    ?????
            other.transform.parent = PreviousParent.transform;
            //other.transform.GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.Impulse);
        }
    }



    public void OnLager() 
    {
        this.GetComponent<LineRenderer>().enabled = true;
        this.GetComponent<LaserPointer>().enabled = true;
    }
}
