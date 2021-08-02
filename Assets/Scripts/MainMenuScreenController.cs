using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class MainMenuScreenController : MonoBehaviour, IRewardedVideoAdListener {
    public Button playButton;
    public Button adButton;
    public Text livesText;
    public Text creditText;

    private int lives;
    private int credits;

    private int firstTime;
    
    void Start() {
        Appodeal.initialize(
            "23158b8d7f17fc82d15209734117c808a4aec946c860f4ac",
            Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO
        );
        Appodeal.setRewardedVideoCallbacks(this);

        playButton.onClick.AddListener(PlayGame);
        adButton.onClick.AddListener(ShowRewardedAD);
        credits = PlayerPrefs.GetInt("credits");
        firstTime = PlayerPrefs.GetInt("first_time");
        if (firstTime == 0) {
            lives = 10;
            PlayerPrefs.SetInt("lives", lives);
            PlayerPrefs.SetInt("first_time", 1);
        } else {
            lives = PlayerPrefs.GetInt("lives");
        }
        livesText.text = "x " + lives.ToString();
        creditText.text = "x " + credits.ToString();
    }

    void Update() {
        
    }

    private void PlayGame() {
        if (lives > 0) {
            lives -= 1;
            PlayerPrefs.SetInt("lives", lives);
            SceneManager.LoadScene("Snake", LoadSceneMode.Single);
        } else if (lives == 0){
            SceneManager.LoadScene("ShopScreen", LoadSceneMode.Single);
        }
    }

    private void ShowRewardedAD() {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)) {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
    }

    #region Rewarded Video callback handlers
    public void onRewardedVideoLoaded(bool isPrecache) {}
    public void onRewardedVideoFailedToLoad() {}
    public void onRewardedVideoShowFailed() {}
    public void onRewardedVideoShown() {}
    public void onRewardedVideoClicked() {}
    public void onRewardedVideoClosed(bool finished) {}
    public void onRewardedVideoFinished(double amount, string name) {
        credits += 10;
        PlayerPrefs.SetInt("credits", credits);
        creditText.text = credits.ToString();
    }
    public void onRewardedVideoExpired() {}
    #endregion
}
