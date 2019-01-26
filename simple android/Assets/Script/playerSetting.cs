using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerSetting : MonoBehaviour {
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(576, 1024, false);
    }
    // Use this for initialization
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(576, 1024, false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
