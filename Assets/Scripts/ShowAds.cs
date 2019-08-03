using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using Mirror;

public class ShowAds : MonoBehaviour
{
    private InterstitialAd interstitial;
    private RewardBasedVideoAd reward;
    private RewardBasedVideoAd energy;
    //private BannerView banner;
    private bool requested = false;
    private bool rewardRequested = false;
    private bool energyRequested = false;

    void Start()
    {
        PlayerPrefs.SetInt("BrawlPro", 1);

        this.reward = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        reward.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        reward.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        reward.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        reward.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        reward.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        reward.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        reward.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        this.RequestReward();

        this.energy = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        energy.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        energy.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoadEnergy;
        // Called when an ad is shown.
        energy.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        energy.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        energy.OnAdRewarded += HandleRewardBasedVideoRewardedEnergy;
        // Called when the ad is closed.
        energy.OnAdClosed += HandleRewardBasedVideoClosedEnergy;
        // Called when the ad click caused the user to leave the application.
        energy.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        //this.RequestEnergy();

        //this.RequestBanner();*/
        //this.RequestInterstitial();

        /*if (PlayerPrefs.HasKey("AdConfig"))
        {
            if (false)//PlayerPrefs.GetInt("AdConfig") == 1)
            {
                //this.banner.Show();
            }
            else
            {
                this.banner.Hide();
            }
        }
        else
        {
            this.banner.Hide();
        }*/
    }

    void Update()
    {
        if (requested)
        {
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
            }
            requested = false;
        }
        if (rewardRequested)
        {
            if (this.reward.IsLoaded())
            {
                this.reward.Show();
            }
            rewardRequested = false;
        }
        if (energyRequested)
        {
            if (this.energy.IsLoaded())
            {
                this.energy.Show();
            }
            energyRequested = false;
        }
    }

    /*public void ShowAd(string id)
    {
        StartCoroutine(WaitForAd(id));
    }*/

    /*IEnumerator WaitForAd(string id)
    {
        while (!Monetization.IsReady(id))
        {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(id) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(AdFinished);
        }
    }*/

    /*void AdFinished(UnityEngine.Monetization.ShowResult result)
    {
        if (result == UnityEngine.Monetization.ShowResult.Finished)
        {
            if (PlayerPrefs.HasKey("Stars"))
            {
                PlayerPrefs.SetInt("Stars", 15 + PlayerPrefs.GetInt("Stars"));
            }
            else
            {
                PlayerPrefs.SetInt("Stars", 15);
            }
        }
    }*/

    private void RequestReward()
    {
        string adUnitId = "ca-app-pub-3563227024265510/2459567290";//"ca-app-pub-3563227024265510/9098624692";
        //string adUnitId = "ca-app-pub-3940256099942544~3347511713"; //test

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.reward.LoadAd(request, adUnitId);
    }

    private void RequestEnergy()
    {
        string adUnitId = "ca-app-pub-3563227024265510/2459567290";//"ca-app-pub-3563227024265510/3973009269";
        //string adUnitId = "ca-app-pub-3940256099942544~3347511713"; //test

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.energy.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoLoaded(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
        this.RequestReward();
    }

    public void HandleRewardBasedVideoFailedToLoadEnergy(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
        //NetworkManager.singleton.StopHost();
    }

    public void HandleRewardBasedVideoOpened(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        this.RequestReward();
    }

    public void HandleRewardBasedVideoClosedEnergy(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        PlayerPrefs.SetInt("Energy", 7);
        PlayerPrefs.SetString("EnergyFullAt", System.DateTime.Now.ToLongTimeString());
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        if (PlayerPrefs.HasKey("Counters"))
        {
            PlayerPrefs.SetInt("Counters", PlayerPrefs.GetInt("Counters") + 125);
        }
        else
        {
            PlayerPrefs.SetInt("Counters", 125);
        }
        FindObjectOfType<CreditsDisplay>().UpdateAmount();
    }

    public void HandleRewardBasedVideoRewardedEnergy(object sender, Reward args)
    {
        PlayerPrefs.SetInt("Energy", 7);
        PlayerPrefs.SetString("EnergyFullAt", System.DateTime.Now.ToLongTimeString());
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    private void RequestInterstitial()
    {
        string adUnitId = "	ca-app-pub-3940256099942544/1033173712";//"ca-app-pub-3563227024265510/2753193296";
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //test

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    /*private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3563227024265510/1203389669";
        //string adUnitId = "ca-app-pub-3940256099942544~3347511713"; //test

        AdSize adSize = new AdSize(400, 55);
        this.banner = new BannerView(adUnitId, adSize, AdPosition.Top);

        // Called when an ad request has successfully loaded.
        this.banner.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.banner.OnAdFailedToLoad += HandleOnAdFailedToLoadB;
        // Called when an ad is shown.
        this.banner.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.banner.OnAdClosed += HandleOnAdClosedB;
        // Called when the ad click caused the user to leave the application.
        this.banner.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.banner.LoadAd(request);
    }*/

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
        this.RequestInterstitial();
    }

    /*public void HandleOnAdClosedB(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        if (banner != null)
        {
            banner.Destroy();
        }
        RequestBanner();
    }*/

    public void HandleOnAdLoaded(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
        this.RequestInterstitial();
    }

    /*public void HandleOnAdFailedToLoadB(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        if (banner != null)
        {
            banner.Destroy();
        }
        this.RequestBanner();
    }*/

    public void HandleOnAdOpened(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdLeavingApplication(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void IntersitialAd()
    {
        //requested = true;
        //Advertisement.Show();
    }

    /*public void StartBanner()
    {
        //this.banner.Show();
    }
    public void HideBanner()
    {
        //this.banner.Hide();
    }*/
    public void Reward()
    {
        rewardRequested = true;
        //ShowAd(placementId);
    }

    public void Energy()
    {
        //energyRequested = true;
        //ShowAd(placementId);
    }
}
