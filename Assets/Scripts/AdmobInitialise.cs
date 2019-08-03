using UnityEngine;
using GoogleMobileAds.Api;
//using UnityEngine.Advertisements;

public class AdmobInitialise : MonoBehaviour
{
    //string gameId = "3150977";
    //bool testMode = false;

    void Start()
    {
        #if UNITY_ANDROID
            string appId = "ca-app-pub-3563227024265510~7903465660";
        #elif UNITY_EDITOR
            string appId = "unused";
        #else
            string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        /*if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, testMode);
        }*/
    }
}
