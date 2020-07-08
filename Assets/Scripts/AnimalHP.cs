using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

            if (HP <= 0)
            {
                if (transform.gameObject.name.Contains("Rabbit"))           // 채력이 0이여서 죽으면 점수처리
                {
                    ScoreManager.instance.SetScore(ScoreManager.instance.rabbitScore);
                }else if (transform.gameObject.name.Contains("Dog"))
                {
                    ScoreManager.instance.SetScore(ScoreManager.instance.dogScore);
                } else if (transform.gameObject.name.Contains("Tiger"))
                {
                    ScoreManager.instance.SetScore(ScoreManager.instance.tigerScore);
                }
                print("채력이 0 죽음");
            }
            print("hit      " + HP );
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
