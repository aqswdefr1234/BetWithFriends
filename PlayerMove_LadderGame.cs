using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_LadderGame : MonoBehaviour
{
    private float speed;
    private float directionX;
    private float directionY;
    private int collisionOrder; // �浹 ������ ���� Ʈ���� �̺�Ʈ�� �ٸ��� �۵��ؾ���.
    private int oneOperation; //Ʈ���Ű� �ѹ��� �ѹ��� �۵��� �� �ֵ���
    private int stopPanel; //�����гο� ����� �� ���� �� �ֵ���
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
         collisionOrder == 0 �� �� Down
        collisionOrder == 1 �� �� left
        collisionOrder == 2 �� �� right
         
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
        if(other.tag == "StickLeftTrigger") //������� ���ʺκ�
        {
            if(oneOperation == 0)
            {
                Debug.Log("�浹");
                if (collisionOrder == 0) //�÷��̾ �������� �� �浹���� ��
                {
                    directionX = 1f;
                    directionY = 0f;
                    collisionOrder = 2;
                    oneOperation = 1;
                }
                if (collisionOrder == 1) //�÷��̾ ���� �������� �����̴� ���� �浹���� ��
                {
                    directionX = 0f;
                    directionY = -1f;
                    collisionOrder = 0;
                    oneOperation = 1;
                }
            }
        }
        if (other.tag == "StickRightTrigger") //������� ������ �κ�
        {
            if (oneOperation == 0)
            {
                Debug.Log("�浹2");
                if (collisionOrder == 0) //�÷��̾ �������� �� �浹���� ��
                {
                    directionX = -1f;
                    directionY = 0f;
                    collisionOrder = 1;
                    oneOperation = 1;
                }
                if (collisionOrder == 2) //�÷��̾ ������ �������� �����̴� ���� �浹���� ��
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
        if (other.tag == "StickLeftTrigger") //������� ���ʺκ�
        {
            oneOperation = 0;
        }
        if (other.tag == "StickRightTrigger") //������� ������ �κ�
        {
            oneOperation = 0;
        }
    }
}
