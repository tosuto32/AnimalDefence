using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    // 태어날때 외부에서 힘을 알려주면 카메라 전방으로 이동하고 싶다.

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(float speed, Vector3 dir)
    {
        //if (rb == null)
        //{
        //    rb = GetComponent<Rigidbody>();
        //}
        //Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up;
        //dir.Normalize();
        //rb.AddForce(dir * speed, ForceMode.Impulse);
    }
}
