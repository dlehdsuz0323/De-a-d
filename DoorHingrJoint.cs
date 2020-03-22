using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class DoorHingrJoint : MonoBehaviour
{
    public static DoorHingrJoint Instance;

    [Range(-360f, 360f)]
    public float fAngle;
    [Range(0.05f, 0.3f)]
    public float fSpeed;

    private Quaternion MaxRotate;

    float time;
    bool isOn;
    bool isFinsh;
    
    

    private void Awake()
    {
        if(DoorHingrJoint.Instance == null)
        {
            DoorHingrJoint.Instance = this;
        }

        MaxRotate = Quaternion.Euler(0, fAngle, 0);
    }

    void Start()
    {
     //   GameSupervisor.Instance.ttt.SetActive(true);
    }


    private void FixedUpdate()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Controller (right)" || other.name == "Controller (left)")
        {
            isOn = true;
        }
    }
    void Update()
    {
        if(isOn)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, MaxRotate, fSpeed);
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }


    void test()
    {
        
    }
}
