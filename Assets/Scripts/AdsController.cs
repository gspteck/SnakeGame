using System;
using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

public class AdsController : MonoBehaviour
{
    public const int RewardedCreditReward = 10;
    public const string AppKey = "23158b8d7f17fc82d15209734117c808a4aec946c860f4ac";

    public static event Action CreditsChanged;

    static AdsController instance;
    bool initialized;

    public static AdsController Ensure()
    {
        if (instance != null)
        {
            return instance;
        }

        var existing = FindAnyObjectByType<AdsController>();
        if (existing != null)
        {
            instance = existing;
            return instance;
        }

        var controllerObject = new GameObject("AdsController");
        return controllerObject.AddComponent<AdsController>();
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    void OnDestroy()
    {
        if (instance != this)
        {
            return;
        }

        AppodealCallbacks.RewardedVideo.OnFinished -= OnRewardedVideoFinished;
        instance = null;
    }

    void Initialize()
    {
        if (initialized)
        {
            return;
        }

        initialized = true;
        Appodeal.DisableNetwork(AppodealNetworks.Admob);
        Appodeal.DisableNetwork(AppodealNetworks.AdmobMediation);
        Appodeal.DisableNetwork(AppodealNetworks.Gam);
        AppodealCallbacks.RewardedVideo.OnFinished += OnRewardedVideoFinished;

        int adTypes = AppodealAdType.Interstitial | AppodealAdType.RewardedVideo;
        Appodeal.Initialize(AppKey, adTypes);
    }

    public static void ShowRewarded()
    {
        Ensure();
        if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo) && Appodeal.CanShow(AppodealAdType.RewardedVideo))
        {
            Appodeal.Show(AppodealShowStyle.RewardedVideo);
        }
    }

    public static void ShowInterstitialIfReady()
    {
        Ensure();
        if (Appodeal.IsLoaded(AppodealAdType.Interstitial) && Appodeal.CanShow(AppodealAdType.Interstitial))
        {
            Appodeal.Show(AppodealShowStyle.Interstitial);
        }
    }

    static void OnRewardedVideoFinished(object sender, RewardedVideoFinishedEventArgs args)
    {
        int credits = PlayerPrefs.GetInt("credits") + RewardedCreditReward;
        PlayerPrefs.SetInt("credits", credits);
        PlayerPrefs.Save();
        CreditsChanged?.Invoke();
    }
}
