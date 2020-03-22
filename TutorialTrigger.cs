using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    None, Drawer, Door,
}
public class TutorialTrigger : MonoBehaviour
{
    public ObjectType type;
    
	void Start ()
    {
		
	}
	

	void Update ()
    {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if((other.name == "Controller (left)" || other.name == "Controller (right)"))
        {
            if(type == ObjectType.Drawer)
            {
                GameSupervisor.Instance.isDrawer[0] = true;
            }
            if(type == ObjectType.Door)
            {
                GameSupervisor.Instance.isDoor[0] = true;
            }
        }
    }
}
