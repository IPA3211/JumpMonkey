using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawn : MonoBehaviour {
    public GameObject banana;
    public GameObject money;
    public float spawnDelay;
    // Use this for initialization

    public void SpawnBanana() {
        StartCoroutine("IESpawnM");
    }

    public void SpawnMoney() {
        money.transform.position = new Vector3(Random.Range(-8.35f, 8.35f), Random.Range(-16.5f, 16.5f), 32.4f);
        money.SetActive(true);
    }

    IEnumerator IESpawnM() {
        yield return new WaitForSeconds(spawnDelay);
        banana.transform.position = new Vector3(Random.Range(-8.35f, 8.35f), Random.Range(-16.5f, 16.5f), 32.4f);
        banana.SetActive(true);
    }
}