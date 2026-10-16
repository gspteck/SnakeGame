using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopScreenController : MonoBehaviour
{
    public Button PurchaseButtonOneLife;
    public Button PurchaseButtonFiftyLives;
    public Button PurchaseButtonOneKLives;
    public Button adButton;
    public Button mainMenuButton;

    private int lives;
    private int credits;

    void OnEnable()
    {
        AdsController.CreditsChanged += RefreshCredits;
    }

    void OnDisable()
    {
        AdsController.CreditsChanged -= RefreshCredits;
    }

    void Start()
    {
        AdsController.Ensure();
        PurchaseButtonOneLife.onClick.AddListener(PurchaseOneLife);
        PurchaseButtonFiftyLives.onClick.AddListener(PurchaseFiftyLives);
        PurchaseButtonOneKLives.onClick.AddListener(PurchaseOneKLives);
        adButton.onClick.AddListener(AdsController.ShowRewarded);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        lives = PlayerPrefs.GetInt("lives");
        RefreshCredits();
    }

    private void PurchaseOneLife()
    {
        if (credits >= 50)
        {
            lives = PlayerPrefs.GetInt("lives");
            lives += 1;
            PlayerPrefs.SetInt("lives", lives);
            credits -= 50;
            PlayerPrefs.SetInt("credits", credits);
            PlayerPrefs.Save();
        }
        else
        {
            AdsController.ShowRewarded();
        }
    }

    private void PurchaseFiftyLives()
    {
        if (credits >= 2000)
        {
            lives = PlayerPrefs.GetInt("lives");
            lives += 50;
            PlayerPrefs.SetInt("lives", lives);
            credits -= 2000;
            PlayerPrefs.SetInt("credits", credits);
            PlayerPrefs.Save();
        }
        else
        {
            AdsController.ShowInterstitialIfReady();
        }
    }

    private void PurchaseOneKLives()
    {
        if (credits >= 35000)
        {
            lives = PlayerPrefs.GetInt("lives");
            lives += 1000;
            PlayerPrefs.SetInt("lives", lives);
            credits -= 35000;
            PlayerPrefs.SetInt("credits", credits);
            PlayerPrefs.Save();
        }
        else
        {
            AdsController.ShowInterstitialIfReady();
        }
    }

    private void RefreshCredits()
    {
        credits = PlayerPrefs.GetInt("credits");
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScreen", LoadSceneMode.Single);
    }
}
