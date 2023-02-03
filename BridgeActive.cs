using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BridgeActive : MonoBehaviour
{
    public TMP_Text playerCount_BattleText; // 마지막 플레이어의 브릿지는 나타내지 않기 위해서
    public GameObject[] bridge = new GameObject[5];
    void Start()
    {
        if(transform.parent.name != playerCount_BattleText.text) //만약 같다면 마지막플레이어 임을 알 수 있다.
        {
            int randomCount = 0;
            for (int i = 0; i < 5; i++)
            {
                randomCount = Random.Range(0, 2); //0, 1 포함
                if (randomCount == 0)
                    bridge[i].SetActive(true);
            }
        }
    }
}
