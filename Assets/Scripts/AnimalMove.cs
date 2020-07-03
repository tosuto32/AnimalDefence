using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 동물들의 이동부분을 담당
// 상태에 따라 행동
// -목적지 찾기
// -이동
// -피격
//

// 목적지
// 속도

public class AnimalMove : MonoBehaviour
{
    NavMeshAgent navMesh;   // 동물의 NavMeshAgent

    public WayPoint wayPoint;   // 경로의 경유지의 Transform의 배열을 가진 객체
    Transform target;           // 목적지를 담을 임시변수
    int targetIndex;            // 경유지 배열을 찾을 인덱스값 변수

    public float speed = 10f;         // 속도를 제어하기위한 변수

    enum State
    {
        Find,                   // 다음 목적지를 찾는 상태
        Move,                   // 목적지까지 이동하는 상태
        Stop                    // 목적지에 도착
    }
    State state;                // 동물의 상태에 변수

    float curTime;
    float endTime = 2;



    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        state = State.Find;         // 시작상태는 Find상태로
        navMesh.speed = 10f;        // 초기 속도는 10f으로
        navMesh.angularSpeed = 500f;// 회전 속도는 500f으로

        targetIndex = 0;            // 배열인덱스값 초기화

        curTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.speed = speed;      // 속도변화를 적용하기위해 업데이트에서 조절
        UpdateStop();               // 상태에따라 아래 함수를 호출함
        UpdateMove();
        UpdateFind();
        print(state);
    }

    private void UpdateStop()
    {
        if (state == State.Stop)
        {
            speed = 0;
            curTime += Time.deltaTime;
            if (curTime > endTime)
            {
                state = State.Find;
                curTime = 0;
            }
        }
    }

    private void UpdateMove()
    {
        if (state == State.Move)
        {
            if (navMesh.remainingDistance <= navMesh.stoppingDistance)  // 남은거리가 멈추는 거리보다 작거나 같으면
            {
                targetIndex++;                                          // 다음 목적지를 설정
                state = State.Find;
            }
        }
    }

    private void UpdateFind()
    {
        if (state == State.Find)                        // 찾기 상태라면
        {
            if (targetIndex >= wayPoint.wp.Length)
            {
                target = Camera.main.transform;
                state = State.Stop;
            }
            if (targetIndex < wayPoint.wp.Length)       // 다음목적지를 설정하고 Move상태로
            {
                target = wayPoint.wp[targetIndex];
                state = State.Move;
            }
            navMesh.destination = target.position;
            speed = 10f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Bullet"))
        {
            state = State.Stop;
        }
    }

    public void SetState(string s)
    {
        if (s == "stop")
        {
            state = State.Stop;
        }
        else if (s == "find")
        {
            state = State.Find;
        }
        else if (s == "move")
        {
            state = State.Move;
        }
        else
        {
            print("잘못된 값이 들어감");
        }
    }
}
