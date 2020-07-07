using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletFactory;
    float curTime;
    public float createTime = 0.3f;

    

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
            if (GameManager.instance.powerUPState == true)
            {
                TrailRenderer tr =  bullet.GetComponent<TrailRenderer>();
                if (tr == null)
                {
                    tr = bullet.AddComponent<TrailRenderer>();
                    tr.startWidth = 0.1f;
                    tr.endWidth = 0.1f;
                }
                tr.enabled = true;
            }

            curTime = 0;

            Destroy(bullet, 3);
        }
    }
}
