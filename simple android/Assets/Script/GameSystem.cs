using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour{

    public int gameLevel = 0;

    public GameObject BGM_Player;
    public GameObject deadMenu;
    public GameObject waitingMenu;
    public GameObject timeText;
    public GameObject ScoreText;

    public static bool isPasued = false;

    private int gameScore;
    private float currentTimeScale;

    private void Start()
    {
        Time.timeScale = 1;
        gameScore = 0;
        StartCoroutine("WaitFor3Second");
    }

    public void SetActiveDeadMenu(bool isActive) {deadMenu.SetActive(isActive);}
    public void LoadScene(string sceneName) {SceneManager.LoadScene(sceneName);}

    public void AddScore(int Score) {
        gameScore += Score;
        if (gameScore > 1000)
        {
            Time.timeScale = 1.1f;
            BGM_Player.GetComponent<AudioSource>().pitch = Time.timeScale;
        }
        if (gameScore > 3000)
        {
            Time.timeScale = 1.2f;
            BGM_Player.GetComponent<AudioSource>().pitch = Time.timeScale;
        }
        if (gameScore > 7000)
        {
            Time.timeScale = 1.3f;
            BGM_Player.GetComponent<AudioSource>().pitch = Time.timeScale;
        }
        if (gameScore > 10000) {
            Time.timeScale = 1.4f;
            BGM_Player.GetComponent<AudioSource>().pitch = Time.timeScale;
        }
        if (gameScore > 15000)
        {
            Time.timeScale = 1.4f;
            BGM_Player.GetComponent<AudioSource>().pitch = Time.timeScale;
        }
        ShowScore();
    }

    public void ShowScore()
    {
        if (gameScore > 999999999)
            ScoreText.GetComponent<TextMesh>().text = "999999999";
        else
            ScoreText.GetComponent<TextMesh>().text = gameScore.ToString();
    }

    IEnumerator WaitFor3Second() {
        isPasued = true;
        waitingMenu.SetActive(true);

        currentTimeScale = Time.timeScale;
        Time.timeScale = 1;

        timeText.GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1f);
        timeText.GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1f);
        timeText.GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1f);

        isPasued = false;
        waitingMenu.SetActive(false);
        Time.timeScale = currentTimeScale;
    }

}
