using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System;

public class AdmobInit : MonoBehaviour, IRewardedVideoAdListener
{
    private float timer;
    private float goal = 5;
    // Start is called before the first frame update
    void Start()
    {
        Appodeal.initialize("8787d18fb2dd41c8dccbf1e30b2352ad488c603553f79bc1", Appodeal.REWARDED_VIDEO, true);
        Appodeal.setRewardedVideoCallbacks(this);
    }

    void Update()
    {
        /*
        if (timer > goal)
        {
            goal = UnityEngine.Random.Range(2, 10);
            timer = 0;
            showRewardedVideo();
        }
        timer += Time.deltaTime;
        */
    }

    public void showRewardedVideo()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
    }

    #region Rewarded Video callback handlers

    public void onRewardedVideoLoaded(bool isPrecache)
    {
        print("Appodeal. Video loaded");
    }

    public void onRewardedVideoFailedToLoad()
    {
        print("Appodeal. Video failed");
    }

    public void onRewardedVideoShowFailed()
    {
        print("Appodeal. RewardedVideo show failed");
    }

    public void onRewardedVideoShown()
    {
        print("Appodeal. Video shown");
    }

    public void onRewardedVideoClosed(bool finished)
    {
        print("Appodeal. Video closed");
    }

    public void onRewardedVideoFinished(double amount, string rewardedName)
    {
        print("Appodeal. Reward: " + amount + " " + rewardedName);
    }

    public void onRewardedVideoExpired()
    {
        print("Appodeal. Video expired");
    }

    public void onRewardedVideoClicked()
    {
        print("Appodeal. Video clicked");
    }

    #endregion
}
