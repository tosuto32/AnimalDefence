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
// -사망

// 목적지
// 속도

public class AnimalMove : MonoBehaviour
{
    NavMeshAgent navMesh;

    public WayPoint wayPoint;
    Transform target;
    int targetIndex;

    public float speed;

    enum State
    {
        Find,
        Move,
        Stop
    }
    State state;


    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        state = State.Find;
        navMesh.speed = 10f;
        navMesh.angularSpeed = 500f;

        targetIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        UpdateFind();

    }


    private void UpdateMove()
    {
        if (state == State.Move)
        {
            print("move");
            if (navMesh.remainingDistance < navMesh.stoppingDistance)
            {

                targetIndex++;
                state = State.Find;
            }
        }
    }

    private void UpdateFind()
    {
        if (state == State.Find)
        {
            print("find");
            print(targetIndex);
            if (targetIndex==wayPoint.wp.Length)
            {
                target = Camera.main.transform;
                state = State.Stop;
            }
            if (targetIndex < wayPoint.wp.Length)
            {
                target = wayPoint.wp[targetIndex];
            }
 
            navMesh.destination = target.position;
            state = State.Move;
        }
    }

}
