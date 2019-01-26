using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(Screen.width, Screen.height * 16 / 9, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
