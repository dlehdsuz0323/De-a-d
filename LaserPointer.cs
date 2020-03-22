using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

    // Use this for initialization
    private SteamVR_TrackedObject trackedObj;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    public LayerMask teleportMask;

    public GameObject disableLaserPrefab;
    private GameObject disableLaser;
    private Transform disableLaserTransform;
    public LayerMask teleportDisableMask;

    private Vector3 hitPoint;

    public Transform cameraRigTransform;
    public GameObject teleportReticleprefab;
    private GameObject reticle;
    private Transform teleportReticleTransform;
    public Transform headTransform;
    public Vector3 teleportReticleOffset;

    private bool shouldTeleport;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
            
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);

        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);

        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
            laserTransform.localScale.y, hit.distance);

        disableLaser.SetActive(false);
    }

    private void ShowDisableLaser(RaycastHit hit)
    {
        disableLaser.SetActive(true);

        disableLaserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        disableLaserTransform.LookAt(hitPoint);

        disableLaserTransform.localScale = new Vector3(disableLaserTransform.localScale.x,
            disableLaserTransform.localScale.y, hit.distance);

        laser.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            RaycastHit hit;

            
            if(Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 10 , teleportDisableMask))
            {
                hitPoint = hit.point;
                ShowDisableLaser(hit);
                shouldTeleport = false;
            }

           else if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 10, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);

                if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
                {
                    reticle.SetActive(true);
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                    shouldTeleport = true;
                }
            }
        }

        else
        {
                disableLaser.SetActive(false);
                laser.SetActive(false);
                reticle.SetActive(false);
            
        }
        if(Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && shouldTeleport)
        {
            StartCoroutine(Fade_Teleport());

        }
	}
    IEnumerator Fade_Teleport()
    {
        SteamVR_Fade.Start(Color.black, 0.3f);
        yield return new WaitForSeconds(0.3f);
        Teleport();
        SteamVR_Fade.Start(Color.clear, 0.3f);
        yield break;
    }

    private void Teleport()
    {
        shouldTeleport = false;
        reticle.SetActive(false);
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = hitPoint + difference;
    }

    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;

        disableLaser = Instantiate(disableLaserPrefab);
        disableLaserTransform = disableLaser.transform;

        reticle = Instantiate(teleportReticleprefab);
        teleportReticleTransform = reticle.transform;
    }
}
