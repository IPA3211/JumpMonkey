using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetchaSystem : MonoBehaviour {

    public GameObject totalObject;
    public GameObject boxcap;
    public GameObject box;
    public GameObject charactor;

    private DataSystem data;

    private bool isCanOpen = false;
    private bool isFinished = false;

    // Use this for initialization
    void Start () {
        StartCoroutine("OnStart");
        data = GameObject.Find("DontDestoryOnLoad").GetComponent<DataSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenBox() {
        if (isCanOpen) {
            StartCoroutine("OnBoxOpen");
            isCanOpen = false;
        }

        if (isFinished) {
            SceneManager.LoadScene("MainScene");
        }
    }

    IEnumerator OnStart() {
        for (int i = 0; i < 100; i++)
        {
            totalObject.transform.position = Vector3.Lerp(totalObject.transform.position, Vector3.zero, 0.05f);
            yield return new WaitForSeconds(0.01f);
        }

        isCanOpen = true;
    }

    IEnumerator OnBoxOpen()
    {
        int selectChar = Random.Range(0, data.charactors.Length);

        charactor.GetComponent<MeshFilter>().mesh = data.charactors[selectChar].charactorMesh;
        charactor.GetComponent<MeshRenderer>().material = data.charactors[selectChar].charactorMat;

        data.charactors[selectChar].isGained = true;
        data.indexCharactor = selectChar;

        data.SaveInfo();

        for (int i = 0; i < 100; i++)
        {
            boxcap.transform.Translate(0, 0.1f, 0);
            box.transform.Translate(0, -0.1f, 0);
            charactor.transform.localScale = Vector3.Lerp(charactor.transform.localScale, new Vector3(2, 2, 2), 0.05f);
            yield return new WaitForSeconds(0.01f);
        }

        isFinished = true;
    }
}
