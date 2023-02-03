using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectBtn : MonoBehaviour
{
    public GameObject meBtn;
    public void PlayerSelectBtn_LadderGame()//메인패널의 플레이어 안에 있는 버튼
    {
        meBtn.SetActive(false);
    }

}
