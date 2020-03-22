using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaPath : MonoBehaviour
{
    public bool IsActive { set; get; }

    public int Count;
    public float CurveValue;
    public float Gravity;
    public Vector3 Velocity;
    public Vector3 GroundPos;
    public List<Transform> RenderList = new List<Transform>();

    private void Awake()
    {
    }
    private void Start()
    {
        CreateRender();
    }
    private void Update()
    {
        if (IsActive)
            ShowPath();
        else
            HidePath();
    }

    private void CreateRender()
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.layer = LayerMask.NameToLayer("Ignore Raycast");
            obj.transform.parent = transform;
            obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            obj.GetComponent<MeshRenderer>().material.color = Color.green;
            Destroy(obj.GetComponent<BoxCollider>());

            RenderList.Add(obj.transform);
            RenderList[i].gameObject.SetActive(false);
        }
    }
    private void HidePath()
    {
        for(int i = 0; i < Count; i++)
        {
            if (!RenderList[i].gameObject.activeSelf)
                continue;
            else
                RenderList[i].gameObject.SetActive(false);
        }
    }
    private void ShowPath()
    {
        if (RenderList.Count == 0)
            CreateRender();

        Vector3 pos = transform.position;
        Vector3 gPos = new Vector3(0, Gravity, 0);
        Velocity = transform.forward * CurveValue;

        for(int i = 0; i < Count; i++)
        {
            // s = s0 + v0t + 1/2gt^2
            float t = i * 0.001f;

            pos = pos + (Velocity * t) + (0.5f * gPos * t * t);
            Velocity += gPos;
            RenderList[i].position = pos;
            RenderList[i].gameObject.SetActive(true);
        }
        CheckGround();
    }
    private void CheckGround()
    {
        int closeIdx = 0;
        float dist = 100;
        RaycastHit hit;
        GroundPos = Vector3.zero;

        for(int i = 0; i < Count; i++)
        {
            if (!RenderList[i].gameObject.activeSelf)
                continue;

            if(Physics.Raycast(RenderList[i].position, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Floor"))
                    continue;

                float curDist = Vector3.Distance(RenderList[i].position, hit.point);

                if (dist < curDist)
                    continue;
                closeIdx = i;
                GroundPos = hit.point;
            }
        }
        for (int i = closeIdx; i < Count; i++)
            RenderList[i].gameObject.SetActive(false);
    }
}
