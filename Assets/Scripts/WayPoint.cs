using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 경유할 포인트의 위치값을 저장하고 있다.
public class WayPoint : MonoBehaviour
{
    public Transform[] wp;

    //// 싱글턴으로 위치값을 가져올수있게 만든다.
    //public static WayPoint instance;

    //private void Awake()
    //{
    //    instance = this;
    //}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform[] GetWayPoint()
    {
        return wp;
    }
}
