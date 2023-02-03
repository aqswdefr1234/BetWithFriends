using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BridgeActive : MonoBehaviour
{
    public TMP_Text playerCount_BattleText; // ������ �÷��̾��� �긴���� ��Ÿ���� �ʱ� ���ؼ�
    public GameObject[] bridge = new GameObject[5];
    void Start()
    {
        if(transform.parent.name != playerCount_BattleText.text) //���� ���ٸ� �������÷��̾� ���� �� �� �ִ�.
        {
            int randomCount = 0;
            for (int i = 0; i < 5; i++)
            {
                randomCount = Random.Range(0, 2); //0, 1 ����
                if (randomCount == 0)
                    bridge[i].SetActive(true);
            }
        }
    }
}
