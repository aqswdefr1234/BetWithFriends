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
    public GameObject playerSelectPanel; //�ش� �гξ��� ��ũ��Ʈ�� �۵���Ű�� ���� ��Ȱ��ȭ ���¿��� Ȱ��ȭ ��Ų��.
    public void PlayerPlusBtn()
    {
        if (playerCountText.text == "2" || playerCountText.text == "3") //�÷��̾� �� ���� 2~4 �̹Ƿ�
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
    public void AfterResultActivePlayerSelectPanel() // ���ʸ�ġ ��ư�� ������ġ ��ư�� ����.
    {
        playerSelectPanel.SetActive(true);
    }
}
