using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
    public GameObject startButton;
    public Transform roulette_Image;
    private float speed; //회전속도
    private float timer;
    private float stopSpeed;
    private float randomPi; // 랜덤한 회전범위
    private float wheelRound; // 원판 회전 수
    private int startTrigger; //1이면 업데이트문 실행
    void Start()
    {
        startTrigger = 0;
        randomPi = Random.Range(1440f, 1800f);
        Debug.Log(randomPi);
        speed = 15f;
        stopSpeed = 7.5f; // speed / 2f  --> 이렇게 하면 스피드가 3초 후에 0이된다.
    }
    void Update()
    {
       if(startTrigger == 1)
        {
            if (randomPi >= wheelRound)
            {
                roulette_Image.Rotate(0f, 0f, 1f * speed);
                wheelRound += speed;
            }
            else
            {
                speed -= stopSpeed * Time.deltaTime;
                roulette_Image.Rotate(0f, 0f, 1f * speed);
                if (speed <= 0f)
                    startTrigger = 0;
            }
                
        }
    }
    public void battleStartBtn()
    {
        startTrigger = 1;
        startButton.SetActive(false);
    }
}
