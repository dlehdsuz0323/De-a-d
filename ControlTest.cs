using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTest : MonoBehaviour
{
    public ParabolaPath parabolaPath;
    public Transform player;
    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device wand;
    void Start()
    {
        player = transform.parent;
        parabolaPath = new ParabolaPath();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (wand.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (parabolaPath == null)
                return;
            else
                parabolaPath.IsActive = true;
        }
        else
        {
            if (parabolaPath == null)
                return;
            else
                parabolaPath.IsActive = false;
            Teleport();
        }

    }
    void Teleport()
    {
        if (parabolaPath.IsActive)
            return;

        Vector3 pos = parabolaPath.GroundPos;
        if (pos == Vector3.zero)
            return;

        player.transform.position = pos;
    }
}
