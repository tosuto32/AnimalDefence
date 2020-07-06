using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 플레이어의 채력을 관리
    // 필살기 버튼을 눌렀을때 일정시간(10초)동안 총알을 거대총알로 변경
    // 필살기 버튼은 30초의 쿨타임을 가지며 활성화될때만 눌리도록 처리
    // 플레이어의 채력이 1감소하면 UI의 표시를 변경
    // 게임오버가 되면 게임오버 UI를 보여줌
    int curHP;
    public int HP
    {
        get { return curHP; }
        set
        {
            curHP -= value;
            // 채력깍일때마다 UI지우기 구현해야함
            if (curHP == 0)
            {
                // 게임오버 UI보여주기
            }
        }
    }
    public RawImage[] lifeUI;
    public Button PowerUpButton;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
