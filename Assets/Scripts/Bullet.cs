﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 총알은 카메라 정면으로 이동한다.
// 이동하다 동물에 부딪히면 동물의 채력을 1깍는다.
public class Bullet : MonoBehaviour
{
    public float speed;
    Vector3 dir;

    Collider col;
    bool crash;
    // Start is called before the first frame update
    void Start()
    {
        dir = Camera.main.transform.forward;
        crash = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        // 총알에 맞은것이 동물이면 채력을 1깍고 채력이 0이라면 파괴한다.
        if (other.gameObject.tag.Contains("Animal"))
        {
            if (crash == false)
            {
                GameObject animal = other.gameObject;                       // animal을 채력을 깍고 속도를 0으로 바꾼다.
                AnimalHP animalHP = animal.GetComponent<AnimalHP>();
                AnimalMove animalMove = animal.GetComponent<AnimalMove>();
                BoxCollider animalCol = animal.GetComponent<BoxCollider>();

                crash = true;

                col = transform.GetComponent<Collider>();
                col.enabled = false;
                animalHP.HP--;
                animalMove.SetState("stop");

                if (animalHP.HP == 0)                                       // 채력이 0이 되면 죽는다.
                {
                    animalCol.enabled = enabled;
                    Destroy(animal, 1);
                }

            }
        }
    }
}
