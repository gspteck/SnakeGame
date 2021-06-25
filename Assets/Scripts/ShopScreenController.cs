using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopScreenController : MonoBehaviour {
    public Button PurchaseButtonOneLife;
    public Button PurchaseButtonFiftyLives;
    public Button PurchaseButtonOneKLives;
    public Button adButton;
    public Button mainMenuButton;

    private int lives;
    private int credits;

    void Start() {
        PurchaseButtonOneLife.onClick.AddListener(PurchaseOneLife);
        PurchaseButtonFiftyLives.onClick.AddListener(PurchaseFiftyLives);
        PurchaseButtonOneKLives.onClick.AddListener(PurchaseOneKLives);
        adButton.onClick.AddListener(ShowRewarderAD);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        lives = PlayerPrefs.GetInt("lives");
        credits = PlayerPrefs.GetInt("credits");
    }

    void Update() {
        
    }

    private void PurchaseOneLife() {
        if (credits >= 50) {
            lives = PlayerPrefs.GetInt("lives");
            lives += 1;
            PlayerPrefs.SetInt("lives", lives);
            credits -= 50;
            PlayerPrefs.SetInt("credits", credits);
            GoToMainMenu();
        } else {
            ShowRewarderAD();
        }
    }

    private void PurchaseFiftyLives() {
        if (credits >= 2000) {
            lives = PlayerPrefs.GetInt("lives");
            lives += 50;
            PlayerPrefs.SetInt("lives", lives);
            credits -= 2000;
            PlayerPrefs.SetInt("credits", credits);
            GoToMainMenu();
        } else {
            //show interstitial
        }
    }

    private void PurchaseOneKLives() {
        if (credits >= 35000) {
            lives = PlayerPrefs.GetInt("lives");
            lives += 1000;
            PlayerPrefs.SetInt("lives", lives);
            credits -= 35000;
            PlayerPrefs.SetInt("credits", credits);
            GoToMainMenu();
        } else {
            //show interstitial
        }
    }

    private void ShowRewarderAD() {
        
    }

    private void GoToMainMenu() {
        SceneManager.LoadScene("MainMenuScreen", LoadSceneMode.Single);
    }
}
