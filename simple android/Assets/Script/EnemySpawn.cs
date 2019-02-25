using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public GameObject rTree1, rTree2, rTree3, rTree4, lTree1, lTree2, lTree3, lTree4;
    public GameObject panzee;
    public Color warningColor;
    public float attackCycle;
    public int attackAmount;

    private bool isFirstPanzee;
    private List<GameObject> treeSet = new List<GameObject>();
    float sumTime;
    // Use this for initialization
    void Start () {
        isFirstPanzee = true;
    }
	
	// Update is called once per frame
	void Update() {
        if (!GameSystem.isPasued)
            sumTime += Time.deltaTime;
        if (sumTime > attackCycle) {
            initList();
            for (int i = 0; i < attackAmount; i++) {
                int targetIndex = Random.Range(0, treeSet.Count);
                StartCoroutine("TreeAttack", treeSet[targetIndex]);
                treeSet.Remove(treeSet[targetIndex]);
            }
            treeSet.Clear();
            sumTime = 0;
            if (GameSystem.gameLevel >= 1) {
                if (isFirstPanzee)
                {
                    SpawnPanzee();
                }
                else if (Random.Range(0, 100) < 30) {
                    SpawnPanzee();
                }
            }
        }

	}

    public void SpawnPanzee() {
        panzee.transform.position = new Vector3(Random.Range(-8.35f, 8.35f), -22, 32.4f);
        int random = Random.Range(0, 1);

        panzee.transform.rotation = Quaternion.Euler(0, 180 * random, 0);

        if (random == 1)
            panzee.GetComponent<PanzeeControl>().isJumpRight = true;
        else
            panzee.GetComponent<PanzeeControl>().isJumpRight = false;

        panzee.GetComponent<PanzeeControl>().isJumpedOnStart = true;
        panzee.SetActive(true);
    }

    IEnumerator TreeAttack(GameObject tree) {
        MeshRenderer rend = tree.GetComponent<MeshRenderer>();
        Color rawColor = rend.material.color;
        float progress = 0;
        for (int i = 0; i < 2; i++)
        {
            progress = 0;
            while (progress <= 1)
            {
                rend.material.color = Color.Lerp(rawColor, Color.red, progress);
                progress += 0.02f;
                yield return new WaitForSeconds(0.005f);
            }
            progress = 0;
            while (progress <= 1)
            {
                rend.material.color = Color.Lerp(Color.red, rawColor, progress);
                progress += 0.02f;
                yield return new WaitForSeconds(0.005f);
            }
        }

        yield return new WaitForSeconds(0.5f);

        progress = 0;

        tree.GetComponent<BoxCollider>().enabled = true;

        while (progress <= 1) {
            tree.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(0, 360, progress), 0);
            progress += 0.02f;
            yield return new WaitForSeconds(0.005f);
        }

        tree.GetComponent<BoxCollider>().enabled = false;
    }

    void initList() {
        treeSet.Add(rTree1);
        treeSet.Add(rTree2);
        treeSet.Add(rTree3);
        treeSet.Add(rTree4);
        treeSet.Add(lTree1);
        treeSet.Add(lTree2);
        treeSet.Add(lTree3);
        treeSet.Add(lTree4);
    }
}
