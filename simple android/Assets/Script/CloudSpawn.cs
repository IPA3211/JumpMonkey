using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour {
    public GameObject cloud;
    private float sumTime;
    private float spawnDelay;
    // Use this for initialization
    void Start () {
        spawnDelay = Random.Range(3, 4);
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameSystem.isPasued)
            sumTime += Time.deltaTime;
        if (sumTime > spawnDelay) {
            StartCoroutine("IESpawnC");
            sumTime = 0;
        }
	}

    IEnumerator IESpawnC()
    {
        Instantiate(cloud, new Vector3(-15, Random.Range(-18f, 18f), Random.Range(35f, 50f)), Quaternion.identity);
        spawnDelay = Random.Range(3, 4);
        yield return 0;
    }
}
