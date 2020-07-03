using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스 오른쪽 버튼을 누르고 카메라를 이동 및 회전하고싶다.
// 이동 WASDQE
// 회전 마우스 입력값
public class ARCameraSim : MonoBehaviour
{
    bool isButtonDown;
    public float speed = 5;
    float rx;
    float ry;
    public float rotSpeed = 200;
    Vector3 originAngle;
    // Start is called before the first frame update
    void Start()
    {
        originAngle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 오르쪽 버튼이 눌리고있다면
        if (Input.GetButtonDown("Fire2"))
        {
            isButtonDown = true;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isButtonDown = false;
        }

        if (isButtonDown)
        {
            Move();
            Rotate();
        }
    }

    private void Rotate()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        rx = Mathf.Clamp(rx, -90, 90);
        transform.eulerAngles = originAngle + new Vector3(-rx, ry, 0);
    }

    private void Move()
    {
        // WSAD : 앞뒤좌우
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // QE // 상하
        float y = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            y = -1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            y = 1;
        }

        Vector3 dir = new Vector3(h, y, v);
        dir.Normalize();
        dir = transform.TransformDirection(dir);
        transform.position += dir * speed * Time.deltaTime;
    }
}
