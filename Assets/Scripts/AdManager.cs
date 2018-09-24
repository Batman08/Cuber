using System;
using System.Collections;
using System.Collections.Generic;
using admob;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private static AdManager Instance { set; get; }

    private readonly string _videoId = "ca-app-pub-1211888741379414/4034801670";
    private readonly string _interstitialId = "ca-app-pub-1211888741379414/1172124172";

    private readonly string _testVideoId = "ca-app-pub-3940256099942544/5224354917";
    private readonly string _testInterstitialId = "ca-app-pub-3940256099942544/1033173712";

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Admob.Instance().initAdmob(_testVideoId, _testInterstitialId);
        Admob.Instance().setTesting(true);
        Admob.Instance().loadInterstitial();
        Admob.Instance().loadRewardedVideo(_testVideoId);

        //Admob.Instance().rewardedVideoEventHandler += OnRewardedVideoEvent;
    }

    private void Update()
    {
        bool hasRewardVideoBeenLoaded = (Admob.Instance().isRewardedVideoReady() == false);
        bool hasInterstitialAdBeenLoaded = (Admob.Instance().isInterstitialReady() == false);

        if (hasRewardVideoBeenLoaded)
        {
            Admob.Instance().loadRewardedVideo(_testVideoId);
        }

        if (hasInterstitialAdBeenLoaded)
        {
            Admob.Instance().loadInterstitial();
        }
    }

    public void ShowVideoAd()
    {
        if (Admob.Instance().isRewardedVideoReady())
        {
            Admob.Instance().showRewardedVideo();
        }
    }

    void OnRewardedVideoEvent(string eventName, string msg)
    {
        Debug.Log("handler onRewardedVideoEvent---" + eventName + "  rewarded: " + msg);
    }

    public void ShowInterstitialAd()
    {
        if (Admob.Instance().isInterstitialReady())
        {
            Admob.Instance().showInterstitial();
        }
    }
}
