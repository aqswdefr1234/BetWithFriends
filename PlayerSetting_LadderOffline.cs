using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random; //System과 UnityEngine 중에 무슨 Random 함수를 사용 할지 정해줌.

public class PlayerSetting_LadderOffline : MonoBehaviour
{
    public TMP_Text playerCountText;
    public TMP_Text playerCountText_Battle_Panel; //배틀패널 상단의 플레이어 수
    public GameObject settingPanel; // 스타트 버튼 누를시 세팅패널이 사라지게 하기 위해서
    public Transform inputFieldPanel; //자식오브젝트를 찾고 활성 및 비활성 하기 위해서
    public GameObject[] players = new GameObject[6]; //플레이어 수에 맞게 배틀필드의 플레이어 오브젝트들을 활성화 시키기 위하여
    public TMP_Text[] selectedOptionsText = new TMP_Text[6]; // 플레이어 패널에 있는 각 플레이어의 text.

    private List<int> randomList = new List<int>(); //인풋필드에 입력한 내용을 사다리의 랜덤한 위치에 배정하기 위해서

    public void PlayerPlusBtn() //플러스 버튼
    {
        if (Convert.ToInt32(playerCountText.text) > 1 && Convert.ToInt32(playerCountText.text) < 6)
        {
            playerCountText.text = Convert.ToString(Convert.ToInt32(playerCountText.text) + 1);
            inputFieldPanel.Find(playerCountText.text).gameObject.SetActive(true);
        }   //총 6명까지.인풋필드패널의 자식을 찾아 활성화 시킨다.gameObject 는 참조되는 대상의 자기자신을 가리킴.
    }
    public void PlayerMinusBtn() //마이너스 버튼
    {
        if (Convert.ToInt32(playerCountText.text) > 2 && Convert.ToInt32(playerCountText.text) < 7)
        {
            inputFieldPanel.Find(playerCountText.text).gameObject.SetActive(false);
            playerCountText.text = Convert.ToString(Convert.ToInt32(playerCountText.text) - 1);
        }
    }
    public void GameStartBtn()
    {
        playerCountText_Battle_Panel.text = playerCountText.text; //활성화 전에 미리 넣어야 브릿지 활성화 할때
        RandomListSelect();                                    //꼬이지 않는다.
        settingPanel.SetActive(false);
    }
    void RandomListSelect()//입력된 값들을 랜덤한 위치에 배정하기
    {
        int convertPlayerCount = Convert.ToInt32(playerCountText.text);
        int random_int = 0;
        for (int i = 0; i < convertPlayerCount; i++)//리스트에 0에서 플레이어 수 - 1 만큼 값을 할당한다.
        {
            randomList.Add(i);
            players[i].SetActive(true);
        }
        for (int j = 0; j < Convert.ToInt32(playerCountText.text); j++) // 리스트에서 랜덤한 인덱스의 값을 뽑고 그 값을 제거한다.
        {
            random_int = Random.Range(0, convertPlayerCount);
            selectedOptionsText[randomList[random_int]].text = inputFieldPanel.Find((j+1).ToString()).gameObject.GetComponent<TMP_InputField>().text;
            randomList.RemoveAt(random_int); // 해당 인덱스 값 제거
            convertPlayerCount--;
        }
    }
}

