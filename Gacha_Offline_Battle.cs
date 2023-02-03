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
            randomCount = Random.Range(1, 16); //1에서 15 까지의 정수(16은 포함 안됨)
        }

        public void WinnerModeBtn()
        {
            selectMode = 0; //위너 모드
            settingPanel.SetActive(false);
        }
        public void LoserModeBtn()
        {
            selectMode = 1; //루저 모드
            settingPanel.SetActive(false);
        }
    }
}

