using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 동물들의 채력을 관리하는 스크립트
public class AnimalHP : MonoBehaviour
{
    float curHp;        // 동물의 현재채력
    public float maxHp; // 최대채력


    public float HP                 // 동물 채력 프로퍼티
    {
        get { return curHp; }
        set
        {
            curHp -= Mathf.Max(0, value);
            //sliderHp.value = curHp;

            if (HP <= 0)    // 채력이 0일때 각 동물마다 점수처리
            {
                if (transform.gameObject.name.Contains("Rabbit"))           
                {
                    ScoreManager.instance.SetScore(ScoreManager.instance.rabbitScore);
                }else if (transform.gameObject.name.Contains("Dog"))
                {
                    ScoreManager.instance.SetScore(ScoreManager.instance.dogScore);
                } else if (transform.gameObject.name.Contains("Tiger"))
                {
                    ScoreManager.instance.SetScore(ScoreManager.instance.tigerScore);
                }
                //print("채력이 0 죽음");
            }
            //print("hit      " + HP );
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;  // 생성될때 현재 채력을 최대채력으로 설정
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
