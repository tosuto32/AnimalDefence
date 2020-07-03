using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderer_Test : MonoBehaviour
{
    LineRenderer lr;
    Vector3 dir;

    public Transform a;
    Transform b;
    Transform c;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        dir = -2*a.transform.up + a.transform.forward;
        dir.Normalize();
        Ray ray = new Ray(a.transform.position, dir);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo, 100))
        {
            lr.SetPosition(0, a.transform.position);
            lr.SetPosition(1, hitInfo.point);
            c.position = hitInfo.point;
        }


    }
}
