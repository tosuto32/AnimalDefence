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

    // c의 위치를 원위치값
    Vector3 cOrigin;



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

    int maxCount = 50;

    LineRenderer lr;

    Vector3[] routeNet;


    // Start is called before the first frame update
    void Start()
    {
        knobOrigin = knob.position;
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.5f;
        lr.endWidth = 0.2f;
        routeNet = new Vector3[maxCount];
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
            lr.enabled = true;
        }
        // 마우스 버튼을 때면 누른 위치에서 땐 위치까지 거리를 측정하고
        else if (Input.GetMouseButtonUp(0))
        {
            endV = Input.mousePosition;

            float distance = Vector3.Distance(startV, endV);
            isDrag = false;
            lr.enabled = false;
            knob.position = knobOrigin;
            c.position = cOrigin;

            // 여기서 포문돌려서 그물을 이동시키자.
            for(int i = 0; i < maxCount; i++)
            {

            }
        }
        
        else if (Input.GetMouseButton(0))
        {
            knob.position = Input.mousePosition;
            Vector3 curV = Input.mousePosition;

            float distance = Vector3.Distance(startV, curV);

            //// 카메라의 살짝 아래방향으로 레이를 쏴서 c의 시작위치를 정한다.
            //dir = -2 * a.transform.up + a.transform.forward*distance/200;
            //dir.Normalize();

            // 카메라의 위치에서 카메라가 보는 전방으로 레이를 쏜다.

            Vector3 CamerP =Camera.main.transform.position;
            // 카메라에서 선이 보이도록 위치를 수정
            CamerP.z += 1;
            CamerP.y -= 2;
            Ray ray = new Ray(CamerP, Camera.main.transform.forward);
            RaycastHit hitInfo;
            // 레이어마스크를 이용하여 바닥에만 레이를 맞춘다.
            int layer = 1 << LayerMask.NameToLayer("Floor");
            if (Physics.Raycast(ray, out hitInfo, 10000, layer))
            {
                print(hitInfo.transform.name);
                // 와이축의 이동을 제한한다.
                Vector3 camForward = Camera.main.transform.forward;
                camForward.y = 0;
                // c의 처음위치 저장
                cOrigin = hitInfo.point;

                // c의 위치이동
                c.position = hitInfo.point + camForward * distance * kAdjustDistance;
                // dir은 실제 레이가 쏘는 방향
                dir = c.position - CamerP;
                dir.Normalize();
                // r은 c의 법선백터
                //Vector3 r = Vector3.Cross(c.position, c.right);
                //r.Normalize();


                // Slerp로 곡선그리기
                Vector3 center = (CamerP + c.position)*0.5f;
                center.y -= 70.0f;
                
                b.position = center;
                Vector3 cameraCenter = CamerP - center;
                Vector3 cCenter = c.position - center;
                lr.positionCount = maxCount;
                for (int i = 0; i<maxCount; i++)
                {
                    lr.positionCount = i + 1;
                    float t = (float)i / maxCount;
                    Vector3 subCurve = Vector3.Slerp(cameraCenter, cCenter, t);
                    lr.SetPosition(i, subCurve+center);
                    // 그물에서 특정지점으로 이동하는 함수를 만들고 routeNet의 값을 포문돌려서 이동시키자.
                    routeNet[i] = lr.GetPosition(i);
                }
                print(hitInfo.point);
            }
        }
    }
}

