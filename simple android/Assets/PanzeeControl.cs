using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanzeeControl : MonoBehaviour {
    
    public float xPower, yPower;
    
    public bool isJumpRight, isJumpedOnStart;
    Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        isJumpRight = true;
        isJumpedOnStart = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (GameSystem.isPasued)
        {
            rigidbody.isKinematic = true;
        }
        else
        {
            rigidbody.isKinematic = false;
            if (!isJumpedOnStart)
            {
                Jump();
                isJumpedOnStart = true;
            }
        }

        if (gameObject.transform.position.y > 20) {
            gameObject.SetActive(false);
        }
    }

    public void Jump()
    {
        if (isJumpRight)
        {
            rigidbody.velocity = new Vector3(xPower, yPower);
            isJumpRight = !isJumpRight;
            StartCoroutine(RotateChar(false));
        }
        else
        {
            rigidbody.velocity = new Vector3(-xPower, yPower);
            isJumpRight = !isJumpRight;
            StartCoroutine(RotateChar(true));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.velocity = new Vector3(0, 0);
        Jump();
    }

    IEnumerator RotateChar(bool isRightTurn)
    {
        if (isRightTurn)
        {
            for (int i = 0; i <= 180; i += 10)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, i, 0);
                yield return new WaitForSeconds(0.001f);
            }
        }
        else
            for (int i = 180; i <= 360; i += 10)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, i, 0);
                yield return new WaitForSeconds(0.001f);
            }
    }
}
