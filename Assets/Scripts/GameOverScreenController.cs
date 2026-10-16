using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreenController : MonoBehaviour
{
    public Text addedCreditsText;
    public Button restartButton;
    public Button mainMenuButton;

    private int lives;
    private int addedCredits;

    void Start()
    {
        AdsController.Ensure();
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        addedCredits = PlayerPrefs.GetInt("added_credits");
        addedCreditsText.text = "+" + addedCredits.ToString() + " credits";
        lives = PlayerPrefs.GetInt("lives");
        StartCoroutine(ShowInterstitialAfterScreenIsVisible());
    }

    private IEnumerator ShowInterstitialAfterScreenIsVisible()
    {
        yield return null;
        AdsController.ShowInterstitialIfReady();
    }

    private void RestartGame()
    {
        if (lives > 0)
        {
            lives -= 1;
            PlayerPrefs.SetInt("lives", lives);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Snake", LoadSceneMode.Single);
        }
        else if (lives == 0)
        {
            SceneManager.LoadScene("ShopScreen", LoadSceneMode.Single);
        }
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScreen", LoadSceneMode.Single);
    }
}
