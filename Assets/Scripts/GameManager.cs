using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 플레이어의 채력을 관리
    // 필살기 버튼을 눌렀을때 일정시간(10초)동안 총알을 거대총알로 변경
    // 필살기 버튼은 30초의 쿨타임을 가지며 활성화될때만 눌리도록 처리
    // 플레이어의 채력이 1감소하면 UI의 표시를 변경
    // 게임오버가 되면 게임오버 UI를 보여줌
    int curHP;                      // 현재 채력
    int hpIndex;                    // 채력 UI배열 인덱스
    public int playerMaxHP;         // 플레이어의 최대채력
    public int HP                   // 플레이어 채력 프로퍼티
    {
        get { return curHP; }
        set
        {
            if (hpIndex < playerMaxHP)
            {
                lifeUI[hpIndex].enabled = false;
                hpIndex++;
                curHP -= value;
            }
            if (curHP == 0)
            {
                print("게임오버됨");
                //게임오버시할것
                gameOverUI.SetActive(true);
                spwanManager.SetActive(false);
                Camera.main.GetComponent<Shoot>().enabled=false;
                GameObject.Find("AR Session Origin").GetComponent<ShootNet>().enabled = false;
                
            }
        }
    }
    public RawImage[] lifeUI;           // 채력 UI이미지
    public Button powerUpButton;        // 파워업 버튼
    public AudioSource powerUPButtonSound;

    public static GameManager instance; // 싱글턴 객체

    public bool powerUPState;           // 파워업 상태

    float curPowerUpTime;               // 파워업 지속시간 체크
    public float powerUpLimitTime = 5;

    public bool coolTimeButtonLock;  // 버튼이 눌렸을때 체크, 눌리면 쿨타임 시작
    float coolTimeButtonLockTime;   // 쿨타임 체크
    public float coolTimeButtonLockLimitTime = 10;

    public Slider powerUPTimeSlider;
    public Image powerUPSliderFillImage;

    public RawImage powerUPButtonImage;
    public Texture[] powerUPTextures;


    public GameObject gameOverUI;
    public AudioSource restartButtonSound;
    public GameObject spwanManager;

    bool gameState;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMaxHP = 3;
        curHP = playerMaxHP;
        hpIndex = 0;
        powerUPTimeSlider.maxValue = powerUpLimitTime;  // 파워업 시간을 설정하고 시작시 비활성화하고
        powerUPTimeSlider.value = powerUpLimitTime;     // 파워업이 활성화 되면 다시 보여지게 한다.
        powerUPSliderFillImage.enabled = false;
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState)
        {
            if (coolTimeButtonLock)
            {

                coolTimeButtonLockTime += Time.deltaTime;
                if (coolTimeButtonLockTime > coolTimeButtonLockLimitTime)
                {

                    // 버튼 다시활성화 하고 버튼눌림상태를 비활성화
                    coolTimeButtonLock = false;
                    powerUpButton.interactable = true;
                    powerUPButtonImage.texture = powerUPTextures[0];
                }
            }

            if (powerUPState)
            {
                powerUPButtonImage.texture = powerUPTextures[1];
                powerUPSliderFillImage.enabled = true;
                curPowerUpTime += Time.deltaTime;
                float temp = powerUPTimeSlider.value;
                temp -= Time.deltaTime;
                if (temp < 0)
                {
                    temp = 0;
                }
                powerUPTimeSlider.value = temp;
                if (curPowerUpTime > powerUpLimitTime)
                {
                    // 파워업상태가 끝남
                    powerUPState = false;

                    powerUPTimeSlider.value = powerUPTimeSlider.maxValue;
                    powerUPSliderFillImage.enabled = false;
                }
            }
        }
     

    }


    // 일정 시간동안 파워업해서 총알에 이펙트를 추가한다.
    public void OnClickPowerUP()
    {
        powerUPButtonSound.Play();
        coolTimeButtonLock = true;
        //// 버튼 비활성화
        powerUpButton.interactable = false;
        powerUPState = true;
        curPowerUpTime = 0;
        coolTimeButtonLockTime = 0;
        // UI를 불타오르게 하는 이펙트 처리... 
    }

    public void OnRestartBt()
    {
        restartButtonSound.Play();
        SceneManager.LoadScene("MainScene");
    }

    public bool GetGameState() {
        return gameState;
    }

    public void SetGameState(bool st)
    {
        gameState = st;
    }
}
