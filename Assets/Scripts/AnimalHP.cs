using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimalHP : MonoBehaviour
{
    float curHp;
    public float maxHp;
    float rabbitScore;
    float dogScore;
    public float HP
    {
        get { return curHp; }
        set
        {
            curHp = Mathf.Max(0, value);
            //sliderHp.value = curHp;

            if (HP == 0)
            {
                if (transform.gameObject.name.Contains("Rabbit"))
                {
                    ScoreManager.instance.SetScore(1);
                }else if (transform.gameObject.name.Contains("Dog"))
                {
                    ScoreManager.instance.SetScore(10);
                }
                print("채력이 0 죽음");
            }
            print("hit      " + HP );
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        HP = maxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
