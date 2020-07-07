using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    // 태어날때 이동할 Vector3[]을 받아서 들어있는 Vector3의 값을 순서대로 이동하자.

    Rigidbody rb;
    public float speed = 5;

    Vector3[] route;
    int targetIndex = 0;

    public GameObject netEffectFactory;

    //MeshRenderer mr;


    // Start is called before the first frame update
    void Start()
    {
        //mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        //Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up;
        //dir.Normalize();
        //rb.AddForce(dir * speed, ForceMode.Impulse);

        //route의 배열에 들어있는 위치들을 경유하여 이동시키자.
        if (targetIndex < route.Length)
        {
            transform.position = Vector3.Lerp(transform.position, route[targetIndex], Time.deltaTime * 50);
            if (Vector3.Distance(transform.position, route[targetIndex]) < 0.5f)
            {
                transform.position = route[targetIndex];
                targetIndex++;
            }
        }
        // 잘못된 코드
        //for (int i = 0; i < route.Length; i++)
        //{
        //    if (transform.position != route[i])
        //    {
        //        transform.position += (route[targetIndex] - transform.position) * speed * Time.deltaTime;
        //    }
        //    else if(transform.position == route[i])
        //    {
        //        targetIndex = i;
        //    }
        //    print(route[i]);
        //}


    }

    // 경로를 가져오는 함수
    public void SetRoute(Vector3[] v)
    {
        route = v;
    }

    private void OnCollisionEnter(Collision other)
    {
        //print(other.gameObject.name+"            넷이 충돌한 물체");
        // 바닥에 닿으면 그곳으로부터 일정범위(Sphere)를 생성한다.
        // 그 범위안에 동물이 있으면 동물의 채력을 3깍는다.
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Destroy(gameObject);
            //mr.enabled = false;
            GameObject netEffect = Instantiate(netEffectFactory);
            netEffect.transform.position = transform.position;
            // 이펙트는 3초후에 삭제
            Destroy(netEffect, 3);
            //Destroy(gameObject, 3.1f);
            // 충돌한 범위 생성
            Collider[] cols = Physics.OverlapSphere(netEffect.transform.position, netEffect.transform.localScale.z / 2);
            for (int i = 0; i < cols.Length; i++)
            {
                //print(cols[i].gameObject.name+"     이펙트에 충돌한것들");
                // 중돌한게 동물tag이면
                if (cols[i].gameObject.tag.Contains("Animal"))
                {

                    // 동물의 값을 변경할 변수 가져오고
                    GameObject animal = cols[i].gameObject;
                    AnimalHP animalHP = animal.GetComponent<AnimalHP>();
                    AnimalMove animalMove = animal.GetComponent<AnimalMove>();
                    BoxCollider animalCol = animal.GetComponent<BoxCollider>();

                    animalHP.HP -= 3;
                    animalMove.SetState("stop");
                    if (animalHP.HP == 0)
                    {
                        animalMove.SetState("die");
                        animalCol.enabled = false;                              // 죽기전에 다른물체와의 충돌을 방지하기위해 콜라이더를 끈다.
                        Destroy(animal, 1.5f);
                    }

                }
            }

        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    print(other.gameObject.name);
    //    // 바닥에 닿으면 그곳으로부터 일정범위(Sphere)를 생성한다.
    //    // 그 범위안에 동물이 있으면 동물의 채력을 3깍는다.
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
    //    {
    //        Destroy(gameObject);
    //        GameObject netEffect = Instantiate(netEffectFactory);
    //        netEffect.transform.position = transform.position;
    //        // 이펙트는 3초후에 삭제
    //        Destroy(netEffect, 3);
    //        // 충돌한 범위 생성
    //        Collider[] cols = Physics.OverlapSphere(netEffect.transform.position, netEffect.transform.localScale.z / 2);
    //        for (int i = 0; i < cols.Length; i++)
    //        {
    //            // 중돌한게 동물tag이면
    //            if (cols[i].gameObject.tag.Contains("Animal"))
    //            {
    //                // 동물의 값을 변경할 변수 가져오고
    //                GameObject animal = other.gameObject;
    //                AnimalHP animalHP = animal.GetComponent<AnimalHP>();
    //                AnimalMove animalMove = animal.GetComponent<AnimalMove>();
    //                BoxCollider animalCol = animal.GetComponent<BoxCollider>();

    //                animalHP.HP -= 3;
    //                animalMove.SetState("stop");

    //                if (animalHP.HP == 0)
    //                {
    //                    animalCol.enabled = false;                              // 죽기전에 다른물체와의 충돌을 방지하기위해 콜라이더를 끈다.
    //                    Destroy(animal, 1);
    //                }

    //            }
    //        }

    //    }

    //}
}
