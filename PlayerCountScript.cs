using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCountScript : MonoBehaviour
{
    public TMP_Text playerCountText;
    public TMP_Text playerCountText_Battle_Panel;
    public GameObject settingPanel;
    public GameObject playerSelectPanel; //해당 패널안의 스크립트를 작동시키기 위해 비활성화 상태에서 활성화 시킨다.
    public void PlayerPlusBtn()
    {
        if (playerCountText.text == "2" || playerCountText.text == "3") //플레이어 수 범위 2~4 이므로
            playerCountText.text = Convert.ToString(Convert.ToInt32(playerCountText.text) + 1);
    }
    public void PlayerMinusBtn()
    {
        if(playerCountText.text == "3" || playerCountText.text == "4")
            playerCountText.text = Convert.ToString(Convert.ToInt32(playerCountText.text) - 1);
    }
    public void GameStartBtn()
    {
        playerCountText_Battle_Panel.text = playerCountText.text;
        playerSelectPanel.SetActive(true);
        settingPanel.SetActive(false);
    }
    public void AfterResultActivePlayerSelectPanel() // 위너매치 버튼과 루저매치 버튼에 들어간다.
    {
        playerSelectPanel.SetActive(true);
    }
}
