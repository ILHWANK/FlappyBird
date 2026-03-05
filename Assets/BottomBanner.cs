using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BottomBanner : MonoBehaviour
{
    private BannerView bannerView;
    
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {
        // 테스트 아이디로 개발 환경에서만 사용하자!
        /* 
         * string adUnitId = "ca-app-pub-3940256099942544/6300978111";
         * 
         */

        #if UNITY_ANDROID
                    string adUnitId = "ca-app-pub-7823245262268168/7015037706";
        #elif UNITY_IPHONE
                    string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
        string adUnitId = "unexpected_platform";
        #endif

        // Clean up banner ad before creating a new one.
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void Update()
    {

    }
}
