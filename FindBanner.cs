using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindBannerScripts
{
    public class FindBanner : MonoBehaviour//Ȩ �¶���, Ȩ �������� ������ ����.
    {
        public static int firstBannerTrigger;
        private GameObject findBanner;
        void Start()
        {
            findBanner = GameObject.Find("GoogleAdMobBanner");
        }
        public void ReturnBtnTriggerPlus()
        {
            firstBannerTrigger = 1; //Ȩ������ ���ư��� �� ���� ����� ����
        }
        public void HideBanner()
        {
            findBanner.GetComponent<banner>().HideBanner();
        }
    }
}