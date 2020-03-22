//using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControllerGrabObject : MonoBehaviour
{
    // Use this for initialization
   
    public static ControllerGrabObject Getinstance;

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;
    // GetPress = SteamVR_Controller.ButtonMask.Grip;

    bool getpress;
    bool getpressup;
    


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }


    void Awake()
    {
        if(ControllerGrabObject.Getinstance == null)
        {
            ControllerGrabObject.Getinstance = this;
        }
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    

    private void SetCollidingObject(Collider col)
    {
        if(collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);  
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;       
        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;

        return fx;
    }

    private void ReleaseObject()
    {
        if(GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity =
                Controller.angularVelocity;
        }
        objectInHand = null;
    }


    // Update is called once per frame
    void Update ()
    {
        //if(Controller.GetHairTriggerDown())
        //      {
        //          if(collidingObject)
        //          {
        //              GrabObject();
        //              Controller.TriggerHapticPulse(500);
        //          }
        //      }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            //  Debug.Log("GRIP BUTTOM DOWN!!!");
            if (collidingObject)
            {
                GrabObject();
            }

            if(collidingObject == null)
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            // Debug.Log("GRIP BUTTOM UP!!!");
            if (objectInHand)
            {
                ReleaseObject();
            }

            if (collidingObject == null)
            {
                if (gameObject.GetComponent<BoxCollider>().isTrigger == false)
                {
                    gameObject.GetComponent<BoxCollider>().isTrigger = true;
                }
            }
        }
        

    }
}
