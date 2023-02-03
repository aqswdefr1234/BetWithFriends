using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random; //System�� UnityEngine �߿� ���� Random �Լ��� ��� ���� ������.

public class PlayerSetting_LadderOffline : MonoBehaviour
{
    public TMP_Text playerCountText;
    public TMP_Text playerCountText_Battle_Panel; //��Ʋ�г� ����� �÷��̾� ��
    public GameObject settingPanel; // ��ŸƮ ��ư ������ �����г��� ������� �ϱ� ���ؼ�
    public Transform inputFieldPanel; //�ڽĿ�����Ʈ�� ã�� Ȱ�� �� ��Ȱ�� �ϱ� ���ؼ�
    public GameObject[] players = new GameObject[6]; //�÷��̾� ���� �°� ��Ʋ�ʵ��� �÷��̾� ������Ʈ���� Ȱ��ȭ ��Ű�� ���Ͽ�
    public TMP_Text[] selectedOptionsText = new TMP_Text[6]; // �÷��̾� �гο� �ִ� �� �÷��̾��� text.

    private List<int> randomList = new List<int>(); //��ǲ�ʵ忡 �Է��� ������ ��ٸ��� ������ ��ġ�� �����ϱ� ���ؼ�

    public void PlayerPlusBtn() //�÷��� ��ư
    {
        if (Convert.ToInt32(playerCountText.text) > 1 && Convert.ToInt32(playerCountText.text) < 6)
        {
            playerCountText.text = Convert.ToString(Convert.ToInt32(playerCountText.text) + 1);
            inputFieldPanel.Find(playerCountText.text).gameObject.SetActive(true);
        }   //�� 6�����.��ǲ�ʵ��г��� �ڽ��� ã�� Ȱ��ȭ ��Ų��.gameObject �� �����Ǵ� ����� �ڱ��ڽ��� ����Ŵ.
    }
    public void PlayerMinusBtn() //���̳ʽ� ��ư
    {
        if (Convert.ToInt32(playerCountText.text) > 2 && Convert.ToInt32(playerCountText.text) < 7)
        {
            inputFieldPanel.Find(playerCountText.text).gameObject.SetActive(false);
            playerCountText.text = Convert.ToString(Convert.ToInt32(playerCountText.text) - 1);
        }
    }
    public void GameStartBtn()
    {
        playerCountText_Battle_Panel.text = playerCountText.text; //Ȱ��ȭ ���� �̸� �־�� �긴�� Ȱ��ȭ �Ҷ�
        RandomListSelect();                                    //������ �ʴ´�.
        settingPanel.SetActive(false);
    }
    void RandomListSelect()//�Էµ� ������ ������ ��ġ�� �����ϱ�
    {
        int convertPlayerCount = Convert.ToInt32(playerCountText.text);
        int random_int = 0;
        for (int i = 0; i < convertPlayerCount; i++)//����Ʈ�� 0���� �÷��̾� �� - 1 ��ŭ ���� �Ҵ��Ѵ�.
        {
            randomList.Add(i);
            players[i].SetActive(true);
        }
        for (int j = 0; j < Convert.ToInt32(playerCountText.text); j++) // ����Ʈ���� ������ �ε����� ���� �̰� �� ���� �����Ѵ�.
        {
            random_int = Random.Range(0, convertPlayerCount);
            selectedOptionsText[randomList[random_int]].text = inputFieldPanel.Find((j+1).ToString()).gameObject.GetComponent<TMP_InputField>().text;
            randomList.RemoveAt(random_int); // �ش� �ε��� �� ����
            convertPlayerCount--;
        }
    }
}

