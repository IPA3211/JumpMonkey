using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobbySystem : MonoBehaviour {

    public GameObject player;
    public float delay;

    private float jumpPosition;
    private float sumTime;
	// Use this for initialization
	void Start () {
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
}
