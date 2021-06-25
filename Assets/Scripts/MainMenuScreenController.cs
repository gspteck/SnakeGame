using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScreenController : MonoBehaviour {
    public Button playButton;
    public Button adButton;
    public Text livesText;
    public Text creditText;

    private int lives;
    private int credits;

    private int firstTime;
    
    void Start() {
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
        PlayerPrefs.DeleteAll();
    }
}
