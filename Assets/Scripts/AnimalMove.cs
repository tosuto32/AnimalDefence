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

    //public WayPoint wayPoint;   // 경로의 경유지의 Transform의 배열을 가진 객체
    Transform[] targets;
    Transform target;           // 목적지를 담을 임시변수
    int targetIndex;            // 경유지 배열을 찾을 인덱스값 변수

    public float speed = 10f;         // 속도를 제어하기위한 변수

    enum State
    {
        Find,                   // 다음 목적지를 찾는 상태
        Move,                   // 목적지까지 이동하는 상태
        Stop,                    // 목적지에 도착 혹은 피격되서 일시적으로 멈춤
        Die                     // 채력이 0일때 죽는 상태

    }
    State state;                // 동물의 상태에 변수

    float curTime;
    float endTime = 2;

    public Animator am;

    AnimalHP animalHP;



    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        state = State.Find;         // 시작상태는 Find상태로
        navMesh.speed = 10f;        // 초기 속도는 10f으로
        navMesh.angularSpeed = 500f;// 회전 속도는 500f으로

        targetIndex = 0;            // 배열인덱스값 초기화

        curTime = 0;

        //wayPoint = GetComponent<WayPoint>();
        //wayPoint.GetWayPoint();

        targets = WayPoint.instance.GetWayPoint();
        am.GetComponent<Animator>();
        animalHP = GetComponent<AnimalHP>();
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.speed = speed;      // 속도변화를 적용하기위해 업데이트에서 조절
        UpdateDie();                // 상태에따라 아래 함수를 호출함
        UpdateStop();
        UpdateMove();
        UpdateFind();
        print(state);
    }

    private void UpdateDie()
    {
        if (state == State.Die)
        {
            speed = 0;
            am.SetTrigger("Dead");
        }
    }

    private void UpdateStop()
    {
        if (state == State.Stop)
        {
            speed = 0;
            curTime += Time.deltaTime;
            am.SetTrigger("Idle");
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
            am.SetTrigger("Run");
            if (navMesh.remainingDistance <= navMesh.stoppingDistance)  // 남은거리가 멈추는 거리보다 작거나 같으면
            {
                targetIndex++;                                          // 다음 목적지를 설정
                state = State.Find;
            }
        }
    }

    // 이동을 직접이동으로 수정해야함

    private void UpdateMove2()
    {
        if (state == State.Move)
        {
            am.SetTrigger("Run");
            // 목적지의 정면을 바라본후에 목적지까지 이동하고 인덱스값을 늘린후 파인드 상태로 전이

        }
    }

    private void UpdateFind()
    {
        if (state == State.Find)                        // 찾기 상태라면
        {
            am.SetTrigger("");
            if (targetIndex >= targets.Length)
            {
                // 최종 목적지에 도착한경우
                target = Camera.main.transform;
                state = State.Stop;

                // 플레이어의 채력을 1깍는다.
                GameManager.instance.HP--;
   
                
            }
            if (targetIndex < targets.Length)       // 다음목적지를 설정하고 Move상태로
            {
                target = targets[targetIndex];
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
        else if (s == "die")
        {
            state = State.Die;
        }
        else
        {
            print("잘못된 값이 들어감");
        }
    }
}
