using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_LadderGame : MonoBehaviour
{
    private float speed;
    private float directionX;
    private float directionY;
    private int collisionOrder; // 충돌 순서에 따라 트리거 이벤트가 다르게 작동해야함.
    private int oneOperation; //트리거가 한번에 한번만 작동할 수 있도록
    private int stopPanel; //스톱패널에 닿았을 때 멈출 수 있도록
    public GameObject meBtn;
    void Start()
    {
        oneOperation = 0;
        speed = 15f;
        directionX = 0f;
        directionY = -1f;
        collisionOrder = 0;
        stopPanel = 0;
        /*
         collisionOrder == 0 일 때 Down
        collisionOrder == 1 일 때 left
        collisionOrder == 2 일 때 right
         
         */
    }

    void FixedUpdate()
    {
        if(meBtn.activeSelf == false && stopPanel == 0)//IsDisabled
        {
            transform.position  +=  new Vector3(directionX * speed, directionY * speed, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "StickLeftTrigger") //막대기의 왼쪽부분
        {
            if(oneOperation == 0)
            {
                Debug.Log("충돌");
                if (collisionOrder == 0) //플레이어가 내려가는 중 충돌했을 때
                {
                    directionX = 1f;
                    directionY = 0f;
                    collisionOrder = 2;
                    oneOperation = 1;
                }
                if (collisionOrder == 1) //플레이어가 왼쪽 방향으로 움직이는 도중 충돌했을 때
                {
                    directionX = 0f;
                    directionY = -1f;
                    collisionOrder = 0;
                    oneOperation = 1;
                }
            }
        }
        if (other.tag == "StickRightTrigger") //막대기의 오른쪽 부분
        {
            if (oneOperation == 0)
            {
                Debug.Log("충돌2");
                if (collisionOrder == 0) //플레이어가 내려가는 중 충돌했을 때
                {
                    directionX = -1f;
                    directionY = 0f;
                    collisionOrder = 1;
                    oneOperation = 1;
                }
                if (collisionOrder == 2) //플레이어가 오른쪽 방향으로 움직이는 도중 충돌했을 때
                {
                    directionX = 0f;
                    directionY = -1f;
                    collisionOrder = 0;
                    oneOperation = 1;
                }
            }
        }
        if (other.tag == "StopMove")
            stopPanel = 1;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "StickLeftTrigger") //막대기의 왼쪽부분
        {
            oneOperation = 0;
        }
        if (other.tag == "StickRightTrigger") //막대기의 오른쪽 부분
        {
            oneOperation = 0;
        }
    }
}
