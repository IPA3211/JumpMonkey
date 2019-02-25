using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour{

    public static int gameLevel = 0;

    public GameObject BGM_Player;
    public GameObject deadMenu;
    public GameObject waitingMenu;
    public GameObject timeText;
    public GameObject ScoreText;
    public GameObject cameraObj;
    public GameObject player;

    public int MoneySpawnScore;
    public Color changeColor;

    public static bool isPasued = false;

    private Camera camera;
    private int gameScore;
    private int tmp_MoneySpawn;
    private float currentTimeScale;
    private Color currentColor;
    private PlayerControler controler;

    private void Start()
    {
        Time.timeScale = 1;
        gameScore = 0;
        StartCoroutine("WaitFor3Second");
        camera = cameraObj.GetComponent<Camera>();
        currentColor = camera.backgroundColor;
        controler = player.GetComponent<PlayerControler>();
    }

    private void Update()
    {
        camera.backgroundColor = Color.Lerp(changeColor, currentColor, controler.getLifeTime() / controler.lifeTime);
    }

    public void SetActiveDeadMenu(bool isActive) {deadMenu.SetActive(isActive);}
    public void LoadScene(string sceneName) {SceneManager.LoadScene(sceneName);}
    public int getScore() { return gameScore; }

    public void AddScore(int Score) {
        gameScore += Score;
        tmp_MoneySpawn += Score;
        if (gameScore > 1000)
        {
            gameLevel = 0;
            Time.timeScale = 1.1f;
            BGM_Player.GetComponent<AudioSource>().pitch = Time.timeScale;
        }
        if (gameScore > 3000)
        {
            gameLevel = 1;
            Time.timeScale = 1.2f;
            BGM_Player.GetComponent<AudioSource>().pitch = Time.timeScale;
        }
        if (gameScore > 7000)
        {
            gameLevel = 2;
            Time.timeScale = 1.3f;
            BGM_Player.GetComponent<AudioSource>().pitch = Time.timeScale;
        }
        if (gameScore > 10000) {
            gameLevel = 3;
            Time.timeScale = 1.4f;
            BGM_Player.GetComponent<AudioSource>().pitch = Time.timeScale;
        }
        if (gameScore > 15000)
        {
            gameLevel = 4;
            Time.timeScale = 1.4f;
            BGM_Player.GetComponent<AudioSource>().pitch = Time.timeScale;
        }

        if (tmp_MoneySpawn > MoneySpawnScore) {
            tmp_MoneySpawn -= MoneySpawnScore;
            gameObject.GetComponent<MoneySpawn>().SpawnMoney();
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

        gameObject.GetComponent<MoneySpawn>().SpawnBanana();
    }

}
