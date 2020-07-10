using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 테스트용으로 만든 스크립트 사용안함
public class ShootNet_v3 : MonoBehaviour
{
    public LineRenderer lr;

    Vector3 center = Vector3.zero;
    Vector3 theArc = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;

        Vector3 targetPoint = Vector3.zero;

        if(playerPlane.Raycast(ray, out hitdist))
        {
            targetPoint = ray.GetPoint(hitdist);

            center = (transform.position + targetPoint) * 0.5f;
            center.y -= 70f;
        }
    }
}
