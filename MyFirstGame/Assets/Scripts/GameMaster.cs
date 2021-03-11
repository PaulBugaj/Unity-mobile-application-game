using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{

    public GameObject restartPanel;

    //public Text timerDisplay;
    public Text score;
    public Text highScore;
    public bool hasLost;

    //public float timer;

    void Start (){
        hasLost = false;
        highScore.text = PlayerPrefs.GetInt("HighScoreValue", 0).ToString();
    }

    private void Update(){
        if (hasLost == false){
            score.text = Time.timeSinceLevelLoad.ToString("F0");
        }
    }

    public void GameOver() {
        hasLost = true;
        Invoke("Delay", 0.7f);
        if ((int)Time.timeSinceLevelLoad > PlayerPrefs.GetInt("HighScoreValue",0)){
            int highScoreValue = (int)Time.timeSinceLevelLoad;
            PlayerPrefs.SetInt("HighScoreValue", highScoreValue);
            highScore.text = highScoreValue.ToString();
        }
    }

    void Delay() {
        restartPanel.SetActive(true);
    }

    public void GotoGameScene() {
        SceneManager.LoadScene("Game");
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToExit() {
        Application.Quit();
    }
}
