using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharViewSystem : MonoBehaviour {

    public GameObject mainObj;
    public GameObject viewObjects;
    public Transform[] viewObjtrans;

    public Camera selectCamera;

    public int focus = 0;
    public GameObject focusedObj = null;

    private GameObject hitObj;
    private DataSystem data;
    private float camera2rect;
    private bool isCamera2;

    private float clickedPositionX;
    
    
    private float sumOfMove;
    

	// Use this for initialization
    void Start () {

        data = GameObject.Find("DontDestoryOnLoad").GetComponent<DataSystem>();
        viewObjtrans = viewObjects.GetComponentsInChildren<Transform>();

        camera2rect = Screen.height * 0.16f;

        for (int i = 0; i < data.charactors.Length; i++){
            if(data.charactors[i].isGained == false){
                viewObjects.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }
        SetFocus(focus);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.y < camera2rect)
            {
                isCamera2 = true;
                clickedPositionX = Input.mousePosition.x;
                viewObjects.GetComponent<Rigidbody>().velocity = Vector3.zero;

                RaycastHit hit;
                Ray ray = selectCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit)) {
                    hitObj = hit.collider.transform.gameObject;
                }

            }
        }

        if (Input.GetMouseButton(0)) {
            if (isCamera2)
            {
                MovePosition(-(clickedPositionX - Input.mousePosition.x) / (Screen.width / 6));
                sumOfMove += Mathf.Abs(clickedPositionX - Input.mousePosition.x);
                clickedPositionX = Input.mousePosition.x;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (isCamera2)
            {
                viewObjects.GetComponent<Rigidbody>().AddForce(-(clickedPositionX - Input.mousePosition.x) * 10, 0, 0);

                if (sumOfMove < 0.1)
                {
                    RaycastHit hit;
                    Ray ray = selectCamera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hitObj.Equals(hit.collider.transform.gameObject))
                        {
                            int f = hitObj.name[hitObj.name.Length - 1] - '0';
                            SetFocus(f);
                        }
                    }
                }
                sumOfMove = 0;

                isCamera2 = false;
                hitObj = null;
            }

        }
        if (viewObjects.transform.position.x < -(viewObjects.transform.childCount-1))
        {
            viewObjects.transform.position += new Vector3(-(viewObjects.transform.childCount - 1) - viewObjects.transform.position.x, 0, 0);
            viewObjects.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (viewObjects.transform.position.x > 0) {
            viewObjects.transform.position += new Vector3(- viewObjects.transform.position.x, 0, 0);
            viewObjects.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void MovePosition(float moveAmount) {
        viewObjects.transform.position += new Vector3(moveAmount, 0);
        sumOfMove += moveAmount;
    }

    public void ReturnHome() {
            SceneManager.LoadScene("MainScene");
    }

    public void SetFocus(int objIndex)
    {
        if (focusedObj != null)
        {
            StartCoroutine(CharScaleSet(false, focusedObj));
        }

        focus = objIndex;
        focusedObj = viewObjects.transform.GetChild(focus).gameObject;

        StartCoroutine(CharScaleSet(true, focusedObj));

        mainObj.GetComponent<MeshFilter>().mesh = focusedObj.GetComponent<MeshFilter>().mesh;
        mainObj.GetComponent<MeshRenderer>().material = focusedObj.GetComponent<MeshRenderer>().material;

        StartCoroutine(MoveviewObjects(viewObjects.transform.position + new Vector3(-objIndex - viewObjects.transform.position.x, 0)));
    }

    public void SetCharactorInfo() {
        if (data.charactors[focus].isGained)
        {
            try
            {
                GameObject.Find("DontDestoryOnLoad").GetComponent<DataSystem>().indexCharactor = focus;
            }
            catch
            {
                Debug.Log("SetCharactorInfo failed");
            }
        }
    }

    IEnumerator CharScaleSet(bool beBigger, GameObject obj) {
        if (beBigger) {
            for (int i = 0; i <= 5; i++)
            {
                obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, new Vector3(1,1,1), 0.2f);
                yield return new WaitForSeconds(0.01f);
            }
            obj.transform.localScale = new Vector3(1, 1, 1);
        }
        else {
            for (int i = 0; i <= 5; i++)
            {
                obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, new Vector3(0.7f, 0.7f, 0.7f), 0.2f);
                yield return new WaitForSeconds(0.01f);
            }
            obj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
    }

    IEnumerator MoveviewObjects(Vector3 newPosition) {
        for (int i = 0; i <= 5; i++)
        {
            viewObjects.transform.position = Vector3.Lerp(viewObjects.transform.position, newPosition, 0.2f);
            yield return new WaitForSeconds(0.01f);
        }
        viewObjects.transform.position = newPosition;
    }
}
