using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총알이 생성되면 할 일을 적은 함수
// 총알은 카메라 정면으로 이동한다.
// 이동하다 동물에 부딪히면 동물의 채력을 1깍는다.
public class Bullet : MonoBehaviour
{
    public float speed;
    Vector3 dir;
    public int attackPower;
    Collider col;
    bool crash;
    public AudioClip[] bulletSoundClip;
    AudioSource bulletSound;


    public GameObject bulletEffectFactory;
    // Start is called before the first frame update
    void Start()
    {
        dir = Camera.main.transform.forward;
        crash = false; // 최근에 충돌한적이 있는지 채크
        if (GameManager.instance.powerUPState)
        {
            attackPower = 3;
        }
        else
        {
            attackPower = 1;
        }
        bulletSound = GetComponent<AudioSource>();
        bulletSound.playOnAwake = false;

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
            bulletSound.clip = bulletSoundClip[Random.Range(0, 2)];
            bulletSound.Play();
            // 다른거와 충돌한적이 없다면
            if (crash == false)
            {
                GameObject animal = other.gameObject;                       // animal을 채력을 깍고 속도를 0으로 바꾼다.
                AnimalHP animalHP = animal.GetComponent<AnimalHP>();
                AnimalMove animalMove = animal.GetComponent<AnimalMove>();
                BoxCollider animalCol = animal.GetComponent<BoxCollider>();

                crash = true;

                GameObject bulletEffect = Instantiate(bulletEffectFactory);
                bulletEffect.transform.position = transform.position;

                col = transform.GetComponent<Collider>();
                col.enabled = false;
                animalHP.HP = attackPower;
                animalMove.SetState("stop");

                if (animalHP.HP == 0)                                       // 채력이 0이 되면 죽는다.
                {
                    animalMove.SetState("die");
                    animalCol.enabled = false;                              // 죽기전에 다른물체와의 충돌을 방지하기위해 콜라이더를 끈다.
                    Destroy(animal, 1.5f);
                }

            }
        }
    }
}
