using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {
    private float sumTime, moveLoop;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (!GameSystem.isPasued)
        {
            gameObject.transform.Translate(0.1f, 0, 0);
            if (gameObject.transform.position.x > 11)
                Destroy(gameObject);
        }
	}
}
