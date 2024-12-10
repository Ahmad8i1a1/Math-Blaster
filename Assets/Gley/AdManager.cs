using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.MobileAds;
using GoogleMobileAds.Common;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public void Awake()
    {
        API.Initialize(OnInitialized);
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnAppStateChanged(AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        Debug.Log("App State is " + state);

        // OnAppStateChanged is not guaranteed to execute on the Unity UI thread.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (state == AppState.Foreground)
            {
                API.ShowAppOpen();
            }
        });
    }

    void OnInitialized() => API.ShowBanner(BannerPosition.Bottom, BannerType.Adaptive);
    // Update is called once per frame

}
