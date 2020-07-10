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


public class AnimalMove : MonoBehaviour
{
    //NavMeshAgent navMesh;   // 동물의 NavMeshAgent

    //public WayPoint wayPoint;   // 경로의 경유지의 Transform의 배열을 가진 객체
    Transform[] targets;        // WayPoint의 배열을 담을 변수
    Transform target;           // 목적지를 담을 임시변수
    int targetIndex;            // 경유지 배열을 찾을 인덱스값 변수

    // 목적지의 방향을 가지고있는 백터
    Vector3 dir;

    // 회전중인지 체크하기위한 bool변수
    //bool checkRotate;

    float speed;         // 속도를 제어하기위한 변수

    string myName;      // 현재 동물의 이름을 가지고 각각 속도를 제어
    float rabbitSpeed = 6;
    float dogSpeed = 4;
    float TigerSpeed = 2;

    enum State
    {
        Find,                   // 다음 목적지를 찾는 상태
        Move,                   // 목적지까지 이동하는 상태
        Stop,                    // 목적지에 도착 혹은 피격되서 일시적으로 멈춤
        Die,                     // 채력이 0일때 죽는 상태
        Roar                    // 호랑이가 샤우트를 함

    }
    State state;                // 동물의 상태에 변수

    float curStopTime;          // 총알이나 폭탄에 맞으면 멈추는 시간체크
    float endStopTime = 2;

    float curRoarTime;          // 로어 모션 시간체크
    float endRoarTime = 1.5f;

    float curRoarCoolTime = 0;      // 로어 쿨타임 체크
    float roarCoolTime = 20;

    public Animator am;         // 동물 에니메이터

    //AnimalHP animalHP;          

    // 플레이어의 채력을 깍은것 체크
    bool checkDamage;           // 목적지에 도착하면 1번만 플레이어에게 데미지를 준다.


    public GameObject buffEfectFactory;     // 호랑이의 스킬이펙트

    AudioSource roarSound;                  // 호랑이의 스킬 효과음
    public AudioClip roarSoundSorce;

    public GameObject wayPoint;             // 경로의 위치를 가지고있는 게임오브젝트

    // Start is called before the first frame update
    void Start()
    {
        //navMesh = GetComponent<NavMeshAgent>();

        SetState("find");         // 시작상태는 Find상태로
        //navMesh.speed = 10f;        // 초기 속도는 10f으로
        //navMesh.angularSpeed = 500f;// 회전 속도는 500f으로

        targetIndex = 0;            // 배열인덱스값 초기화

        curStopTime = 0;            // 시간값들 초기화
        curRoarTime = 0;
        //wayPoint = GetComponent<WayPoint>();
        //wayPoint.GetWayPoint();
        wayPoint = GameObject.Find("WayPoint");     
        WayPoint wp = wayPoint.GetComponent<WayPoint>();
        targets = wp.GetWayPoint();  // 경유지 좌표 가져오기
        am.GetComponent<Animator>();
        //animalHP = GetComponent<AnimalHP>();

        checkDamage = true;

        // 자신의 이름을 가져와서 이름에따라 속도 정하기
        myName = gameObject.name;               // 호랑이일때만 스킬사용시 필요한 사운드소스 초기화
        if (myName.Contains("Tiger"))
        {
            roarSound = gameObject.AddComponent<AudioSource>();
            roarSound.playOnAwake = false;
            roarSound.clip = roarSoundSorce;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.gameObject.name.Contains("Tiger"))        // 호랑이일때만 스킬사용 쿨타임체크
        {
            curRoarCoolTime += Time.deltaTime;
            if (curRoarCoolTime > roarCoolTime)
            {
                curRoarCoolTime = 0;
                SetState("roar");
            }
        }
        //navMesh.speed = speed;      // 속도변화를 적용하기위해 업데이트에서 조절
        UpdateFind();                   // 상태에따라 아래 함수를 호출함
        UpdateMove2();
        UpdateStop();
        UpdateRoar();
        UpdateDie();                
        //UpdateMove();         // 현재는 미사용 move2로 대체
        //print(state);   //상태체크용 프린트
    }

    private void UpdateRoar()               // 호랑이 스킬사용후 찾기상태로 전이
    {
        if (state == State.Roar)
        {
            speed = 0;
            curRoarTime += Time.deltaTime;

            if (curRoarTime > endRoarTime)
            {
                SetState("find");
                curRoarTime = 0;
            }
        }
    }

    private void Roar()             
    {
        // 호랑이가 자신주위의 범위안에 동물들의 스피드를 3 증가시킨다.
        roarSound.Play();
        Collider[] cols = Physics.OverlapSphere(transform.position, 7.5f);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag.Contains("Animal"))
            {
                cols[i].gameObject.GetComponent<AnimalMove>().AddSpeed(3);
                GameObject buffEffect = Instantiate(buffEfectFactory);
                buffEffect.transform.position = cols[i].transform.position + Vector3.up * 3;
            }
        }



    }
    private void AddSpeed(float addSpeed)       // 해당동물에 스피드를 올린다.
    {
        if (myName.Contains("Rabbit"))
        {
            rabbitSpeed += addSpeed;
        }
        else if (myName.Contains("Dog"))
        {
            dogSpeed += addSpeed;
        }
        else if (myName.Contains("Tiger"))
        {
            TigerSpeed += addSpeed;
        }
    }


    private void UpdateDie()        // 죽는상태
    {
        if (state == State.Die)
        {

        }
    }

    private void UpdateStop()       // 총알이나 폭탄에 맞을때 멈춘상태
    {
        if (state == State.Stop)
        {
            curStopTime += Time.deltaTime;

            if (curStopTime > endStopTime)
            {
                SetState("find");
                curStopTime = 0;
            }
        }
    }


    //private void UpdateMove()
    //{
    //    if (state == State.Move)
    //    {
    //        am.SetTrigger("Run");
    //        if (navMesh.remainingDistance <= navMesh.stoppingDistance)  // 남은거리가 멈추는 거리보다 작거나 같으면
    //        {
    //            targetIndex++;                                          // 다음 목적지를 설정
    //            state = State.Find;
    //        }
    //    }
    //}

    // 이동을 직접이동으로 수정해야함

    private void UpdateMove2()          // 다음목적지로 이동하는 상태
    {
        if (state == State.Move)
        {
            //am.SetTrigger("Run");
            am.SetTrigger("Run");
            // 목적지의 정면을 바라본후에 목적지까지 이동하고 인덱스값을 늘린후 파인드 상태로 전이

            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                targetIndex++;
                SetState("find");
            }
            transform.position += dir * speed * Time.deltaTime;
        }
    }

    private void UpdateFind()
    {
        if (state == State.Find)                        // 찾기 상태라면
        {
            if (targetIndex >= targets.Length)
            {
                // 최종 목적지에 도착한경우
                target = Camera.main.transform;


                // 플레이어의 채력을 1깍는다.
                if (GameManager.instance.HP != 0 && checkDamage)
                {
                    GameManager.instance.HP =1;
                    checkDamage = false;
                }
                Destroy(gameObject, 1);


            }
            if (targetIndex < targets.Length)       // 다음목적지를 설정하고 회전한 후에 Move상태로
            {
                target = targets[targetIndex];
                dir = target.position - transform.position;
                dir.Normalize();

                transform.forward = dir;

                SetState("move");
            }

            //navMesh.destination = target.position;
        }
    }

    private void OnCollisionEnter(Collision other)          // 총알에 맞을때 스톱상태로
    {
        if (other.gameObject.name.Contains("Bullet"))
        {
            SetState("stop");
        }
    }

    // 함수를 이용해서 상태변환
    public void SetState(string s)
    {
        if (s == "stop")
        {
            state = State.Stop;
            speed = 0;
            am.SetTrigger("Stop");
        }
        else if (s == "find")
        {
            state = State.Find;
            speed = 0;
            am.SetTrigger("Idle");
        }
        else if (s == "move")
        {
            state = State.Move;
            if (myName.Contains("Rabbit"))
            {
                speed = rabbitSpeed;
            }
            else if (myName.Contains("Dog"))
            {
                speed = dogSpeed;
            }
            else if (myName.Contains("Tiger"))
            {
                speed = TigerSpeed;
            }
            am.SetTrigger("Run");
        }
        else if (s == "die")
        {
            state = State.Die;
            speed = 0;
            am.SetTrigger("Dead");
        }
        else if (s == "roar")
        {
            state = State.Roar;
            speed = 0;
            Roar();
            am.SetTrigger("Roar");
        }
        else
        {
            print("잘못된 값이 들어감");
        }
    }
}
