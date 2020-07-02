using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimalHP : MonoBehaviour
{
    public float curHp;
    float maxHp;
    public float HP
    {
        get { return curHp; }
        set
        {
            curHp = Mathf.Max(0, value);
            //sliderHp.value = curHp;
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
