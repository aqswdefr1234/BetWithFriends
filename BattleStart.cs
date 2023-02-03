using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
    public GameObject startButton;
    public Transform roulette_Image;
    private float speed; //ȸ���ӵ�
    private float timer;
    private float stopSpeed;
    private float randomPi; // ������ ȸ������
    private float wheelRound; // ���� ȸ�� ��
    private int startTrigger; //1�̸� ������Ʈ�� ����
    void Start()
    {
        startTrigger = 0;
        randomPi = Random.Range(1440f, 1800f);
        Debug.Log(randomPi);
        speed = 15f;
        stopSpeed = 7.5f; // speed / 2f  --> �̷��� �ϸ� ���ǵ尡 3�� �Ŀ� 0�̵ȴ�.
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
