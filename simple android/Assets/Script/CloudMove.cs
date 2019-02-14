using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {
	// Use this for initialization
	void Start () {
        gameObject.transform.position = new Vector3(transform.position.x, Random.Range(-18f, 18f), Random.Range(35f, 50f));
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameSystem.isPasued)
        {
            gameObject.transform.Translate(0.1f, 0, 0);
            if (gameObject.transform.position.x > 14)
                gameObject.transform.position = new Vector3(-15, Random.Range(-18f, 18f), Random.Range(35f, 50f));
        }
	}
}
