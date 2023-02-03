using System;
using UnityEngine;
using GoogleMobileAds.Api;
using FindBannerScripts;

public class banner : MonoBehaviour
{
    BannerView bannerView;
    
    public void Start()
    {
        if(FindBanner.firstBannerTrigger == 0)
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(initStatus => { });
            this.RequestBanner();
            PlayerPrefs.SetInt("AdsLoad", 1);
        }
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
            string adUnitId = "";
#elif UNITY_IPHONE
            string adUnitId = "";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
        
    }
    public void DontDestroyBtnEvent()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void HideBanner()
    {
        this.bannerView.Hide();
    }
    public void ShowBanner()
    {
        this.bannerView.Show();
    }
}
