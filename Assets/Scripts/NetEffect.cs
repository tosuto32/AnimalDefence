using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Animal"))
        {

            // 동물의 값을 변경할 변수 가져오고
            GameObject animal = other.gameObject;
            AnimalHP animalHP = animal.GetComponent<AnimalHP>();
            AnimalMove animalMove = animal.GetComponent<AnimalMove>();
            BoxCollider animalCol = animal.GetComponent<BoxCollider>();

            animalHP.HP -= 3;
            animalMove.SetState("stop");
            if (animalHP.HP == 0)
            {
                animalMove.SetState("die");
                animalCol.enabled = false;                              // 죽기전에 다른물체와의 충돌을 방지하기위해 콜라이더를 끈다.
                Destroy(animal, 1.5f);
            }

        }
    }
}
