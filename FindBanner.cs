using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindBannerScripts
{
    public class FindBanner : MonoBehaviour//홈 온라인, 홈 오프라인 씬에서 실행.
    {
        public static int firstBannerTrigger;
        private GameObject findBanner;
        void Start()
        {
            findBanner = GameObject.Find("GoogleAdMobBanner");
        }
        public void ReturnBtnTriggerPlus()
        {
            firstBannerTrigger = 1; //홈씬으로 돌아갔을 때 광고 재실행 방지
        }
        public void HideBanner()
        {
            findBanner.GetComponent<banner>().HideBanner();
        }
    }
}