using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBanner : MonoBehaviour //���� �� ���� ��� ����.
{
    private GameObject findBanner;
    void Start()
    {
        findBanner = GameObject.Find("GoogleAdMobBanner");
    }
    public void IfReturnBtnClick() //�ڷΰ��� ��ư ������ �ٽ� ��� ���̰� �ϱ�
    {
        findBanner.GetComponent<banner>().ShowBanner();
    }
}
