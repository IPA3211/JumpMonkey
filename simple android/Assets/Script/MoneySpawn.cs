using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawn : MonoBehaviour {
    public GameObject money;
    public float spawnDelay;
    // Use this for initialization

    public void SpawnMoney() {
        StartCoroutine("IESpawnM");
    }

    IEnumerator IESpawnM() {
        yield return new WaitForSeconds(spawnDelay);
        money.transform.position = new Vector3(Random.Range(-8.35f, 8.35f), Random.Range(-16.5f, 16.5f), 32.4f);
        money.SetActive(true);
    }
}