using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysRotation : MonoBehaviour {
    private float sumTime;
    public float speed = 1;
    public float rot;
	// Use this for initialization
	void Start () {
        rot = transform.rotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameSystem.isPasued) {
            rot += Time.deltaTime * 100 * speed;
            transform.rotation = Quaternion.Euler(0, rot, 0);
        }
	}
}
