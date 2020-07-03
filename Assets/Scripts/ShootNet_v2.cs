using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNet_v2 : MonoBehaviour
{
    // net를 bezier 곡선으로 네트의 조준선을 표시하고 조준선대로 net를 날리고싶다.
    // 1. 조준선그리기 
    // 2. c의 위치에 net 범위 표시하기
    // 3. c의 위치를 드래그값으로 변경하기 >레이를 아래방향으로 쏘고 드래그값에따라 레이의 방향을 위로 올리기

    public Transform A;
    public Transform B;
    public Transform C;
    public int maxCount = 50;

    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i <= maxCount; i++)
        {
            lr.positionCount = i + 1;
            float t = (float)i / maxCount;
            Vector3 pos = Curve(A.position, B.position, C.position, t);
            lr.SetPosition(i, pos);
        }
    }

    Vector3 Curve(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        //a와 b의 중간위치값과 b와c의 중간값의 점을 연결해서 그값을 다시 중간값의 점
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, t);
    }
}
