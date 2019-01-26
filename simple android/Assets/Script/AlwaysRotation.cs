using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysRotation : MonoBehaviour {
    private float sumTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameSystem.isPasued)
            transform.rotation = Quaternion.Euler(0, Time.realtimeSinceStartup * 100, 0);
	}
}
