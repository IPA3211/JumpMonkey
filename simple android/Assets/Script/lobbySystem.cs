using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lobbySystem : MonoBehaviour {

    public GameObject player;
    public float delay;
    public GameObject moneyText;

    public int MoneyNeeds = 100;

    private float jumpPosition;
    private float sumTime;
	// Use this for initialization
	void Start () {
        ShowMoney();
        delay = 1;
        jumpPosition = Random.Range(-15, 15);
    }
	
	// Update is called once per frame
	void Update () {
        sumTime += Time.deltaTime;
        if (player.transform.position.y < jumpPosition && sumTime > delay)
        {
            jumpPosition = Random.Range(-15, 15);
            player.GetComponent<PlayerControler>().Jump();
            sumTime = 0;
        }
	}

    public void ShowMoney()
    {
        moneyText.GetComponent<Text>().text = DataSystem.gainedMoney.ToString();

    }

    public void LoadgatchaView() {
        if (DataSystem.gainedMoney >= MoneyNeeds) {
            DataSystem.gainedMoney -= MoneyNeeds;
            SceneManager.LoadScene("getchaScene");
        }
    }

}
