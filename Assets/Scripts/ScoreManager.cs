using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 적을 죽일때마다 점수를 획득한다.
    // 점수를 택스트에 표시한다.
    public Text scoreText;

    int score;

    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE : ";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE : " + score;
    }

    public int SetScore(int v)
    {
        score += v;
        return score;
    }
}
