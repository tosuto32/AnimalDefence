using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletFactory;
    float curTime;
    public float createTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        curTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > createTime)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = Camera.main.transform.position;
            bullet.transform.forward = Camera.main.transform.forward;

            curTime = 0;
        }
    }
}
