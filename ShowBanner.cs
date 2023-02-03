using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBanner : MonoBehaviour //게임 씬 마다 들어 있음.
{
    private GameObject findBanner;
    void Start()
    {
        findBanner = GameObject.Find("GoogleAdMobBanner");
    }
    public void IfReturnBtnClick() //뒤로가기 버튼 누를시 다시 배너 보이게 하기
    {
        findBanner.GetComponent<banner>().ShowBanner();
    }
}
