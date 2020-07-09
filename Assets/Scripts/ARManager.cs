using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    ARRaycastManager arRaycastManager;
    Vector2 center;
    public GameObject indicator;
    public GameObject map;
    

    bool mapCreateCheck;

    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        mapCreateCheck = false;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateIndicator();  // 인디케이터 만드는 부분
        UpdateMapCreate();  // 인디케이터 터치할경우 맵생성
    }

    private void UpdateIndicator()
    {
        if (mapCreateCheck == false)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            
            if (arRaycastManager.Raycast(center, hits))
            {
                indicator.SetActive(true);
                indicator.transform.position = hits[0].pose.position;
            }
            else
            {
                indicator.SetActive(false);
            }

        }
    }

    private void UpdateMapCreate()
    {
        if (indicator.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.gameObject == indicator)
                    {
                        if (mapCreateCheck == false)
                        {
                            map.transform.position = hitInfo.point+Vector3.down*15+Vector3.forward * 20;
                            

                            map.SetActive(true);
                            mapCreateCheck = true;
                            GameManager.instance.SetGameState(true);
                            indicator.SetActive(false);
                        }
                    }

                }
            }
        }
    }
}
