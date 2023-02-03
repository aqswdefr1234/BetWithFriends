using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gacha_Offline
{
    public class Gacha_Offline_Battle : MonoBehaviour
    {
        public GameObject settingPanel;
        public static int randomCount;
        public static int selectMode;

        void Start()
        {
            selectMode = 0;
            randomCount = Random.Range(1, 16); //1���� 15 ������ ����(16�� ���� �ȵ�)
        }

        public void WinnerModeBtn()
        {
            selectMode = 0; //���� ���
            settingPanel.SetActive(false);
        }
        public void LoserModeBtn()
        {
            selectMode = 1; //���� ���
            settingPanel.SetActive(false);
        }
    }
}

