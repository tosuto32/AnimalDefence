using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ShootNet : MonoBehaviour
{
    // 시작위치
    Vector3 startV;
    // 종료위치
    Vector3 endV;

    Vector3 dir;

    float dis;


    // 그물공장
    public GameObject netFactory;
    
    public RectTransform knob;

    public Vector3 knobOrigin;

    public float kAdjustBallSpeed = 0.025f;

    public float kAdjustDistance = 0.002f;

    bool isDrag;

    public Transform a;
    public Transform b;
    public Transform c;

    Transform cOrigin;

    int maxCount = 50;

    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        knobOrigin = knob.position;
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.5f;
        lr.endWidth = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {


        // 마우스 버튼을 누르면 그 위치를 기억하고
        if (Input.GetMouseButtonDown(0))
        {
            startV = Input.mousePosition;
            knob.position = Input.mousePosition;
            isDrag = true;
        }
        // 마우스 버튼을 때면 누른 위치에서 땐 위치까지 거리를 측정하고
        else if (Input.GetMouseButtonUp(0))
        {
            endV = Input.mousePosition;

            float distance = Vector3.Distance(startV, endV);
            isDrag = false;
            lr.enabled = false;
            knob.position = knobOrigin;
        }
        
        else if (Input.GetMouseButton(0))
        {
            knob.position = Input.mousePosition;
            Vector3 curV = Input.mousePosition;

            float distance = Vector3.Distance(startV, curV);

            //// 카메라의 살짝 아래방향으로 레이를 쏴서 c의 시작위치를 정한다.
            //dir = -2 * a.transform.up + a.transform.forward*distance/200;
            //dir.Normalize();
            
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Floor");
            if (Physics.Raycast(ray, out hitInfo, 10000, layer))
            {
                print(hitInfo.transform.name);
                Vector3 camForward = Camera.main.transform.forward;
                camForward.y = 0;
                c.position = hitInfo.point + camForward * distance * kAdjustDistance;
                dir = c.position - Camera.main.transform.position;
                dir.Normalize();
                Vector3 r = Vector3.Cross(c.position, c.right);
                r.Normalize();
                b.position = r * 10;
                print(hitInfo.point);
            }
            lr.SetPosition(0, Camera.main.transform.position);
            lr.SetPosition(1, c.position);

        }
    }
}

