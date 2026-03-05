using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class ReStart : MonoBehaviour
{
    private InterstitialAd interstitial;

    public Canvas myCanvas;

    public void restart() {
        
        RequestInterstitial();

        StartCoroutine(showInterstitial());

        IEnumerator showInterstitial()
        {
            while (!this.interstitial.IsLoaded())
            {
                Debug.Log("±¤°í ·ÎµùÁß...");
                yield return new WaitForSeconds(0.2f);
            }
            this.interstitial.Show();
            myCanvas.sortingOrder = -1;

        }
    }

    private void RequestInterstitial()
    {

        /*
         * Å×½ºÆ® ±¤°í ID
         * string adUnitId = "ca-app-pub-3940256099942544/1033173712";
         */

        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-7823245262268168/1762711025";
        #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
                string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

    }

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        Score.score = 0;
        SceneManager.LoadScene("PlayScene");
    }


}
