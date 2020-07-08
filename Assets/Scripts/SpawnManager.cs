using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // 일정 시간마다 각각의 리스폰타임을 가지는 동물들을 소환한다.

    float curRabbitTime = 0;
    float createRabbitTime=2;
    float curDogTime=0;
    float createDogTime = 5;
    float curTigerTime = 0;
    float createTigerTime = 20;

    public Transform spawnPoint;

    public GameObject rabbitFactory;
    public GameObject dogFactory;
    public GameObject tigerFactory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curRabbitTime += Time.deltaTime;
        if (curRabbitTime > createRabbitTime)
        {
            GameObject rabbit = Instantiate(rabbitFactory);
            rabbit.transform.position = spawnPoint.position;
            curRabbitTime = 0;
        }
        curDogTime += Time.deltaTime;
        if (curDogTime > createDogTime)
        {
            GameObject dog = Instantiate(dogFactory);
            dog.transform.position = spawnPoint.position;
            curDogTime = 0;
        }
        curTigerTime += Time.deltaTime;
        if (curTigerTime > createTigerTime)
        {
            GameObject tiger = Instantiate(tigerFactory);
            tiger.transform.position = spawnPoint.position;
            curTigerTime = 0;
        }

    }
}
