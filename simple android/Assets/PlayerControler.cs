using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : SwipeReceiver
{

    bool isJumpRight,
    isCanJump,
    isCanDashUp,
    isCanDashDown,
    isCanDashLeft,
    isCanDashRight,
    isDashNow,
    isOnWall;


    public float xPower, yPower, DragSpeed;
    public float dashPower;

    public GameObject leftParticle, rightParticle, dashParticle;

    private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true);
        rigidbody = gameObject.GetComponent<Rigidbody>();

        rigidbody.velocity = new Vector3(-xPower, yPower);
        isJumpRight = !isJumpRight;
        isCanJump = false;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if ((Input.GetMouseButton(0)||Input.GetKey("space")) && isCanJump)
        {
            if (isJumpRight)
            {
                rigidbody.velocity = new Vector3(xPower, yPower);
                isJumpRight = !isJumpRight;
                isCanJump = false;
            }
            else
            {
                rigidbody.velocity = new Vector3(-xPower, yPower);
                isJumpRight = !isJumpRight;
                isCanJump = false;
            }
        }

        if (!isDashNow && !isOnWall)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && isCanDashUp)
            {
                StartCoroutine(Dash(1));
                isCanDashUp = false;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && isCanDashDown)
            {
                StartCoroutine(Dash(2));
                isCanDashDown = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && isCanDashLeft)
            {
                StartCoroutine(Dash(3));
                isCanDashLeft = false;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && isCanDashRight)
            {
                StartCoroutine(Dash(4));
                isCanDashRight = false;
            }
        }


        if (rigidbody.velocity.y < -DragSpeed)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -DragSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy")){

            OnEnemyHit();
        }

        rigidbody.velocity = new Vector3(0, 0);
        isCanJump = true;
        isCanDashUp = true;
        isCanDashDown = true;
        isCanDashLeft = true;
        isCanDashRight = true;
        isOnWall = true;

        if (collision.gameObject.name == "wall_left")
        {
            leftParticle.GetComponent<ParticleSystem>().Play(true);
        }
        else if (collision.gameObject.name == "wall_right")
        {
            rightParticle.GetComponent<ParticleSystem>().Play(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isOnWall = false;
        leftParticle.GetComponent<ParticleSystem>().Stop(false);
        rightParticle.GetComponent<ParticleSystem>().Stop(false);
    }

    protected override void OnSwipeUp()
    {
        if (!isDashNow && !isOnWall)
        {
            if (isCanDashUp)
            {
                StartCoroutine(Dash(1));
                isCanDashUp = false;
            }
        }
    }

    protected override void OnSwipeDown()
    {
        if (!isDashNow && !isOnWall)
        {
            if (isCanDashDown)
            {
                StartCoroutine(Dash(2));
                isCanDashDown = false;
            }
        }
    }

    protected override void OnSwipeLeft()
    {
        if (!isDashNow && !isOnWall)
        {
            if (isCanDashLeft)
            {
                StartCoroutine(Dash(3));
                isCanDashLeft = false;
            }
        }
    }

    protected override void OnSwipeRight(){
        if (!isDashNow && !isOnWall)
        {
            if (isCanDashRight)
            {
                StartCoroutine(Dash(4));
                isCanDashRight = false;
            }
        }
    }

    public void OnEnemyHit(){

        
    }



    IEnumerator Dash(int commend)
    {
        isDashNow = true;
        Vector3 currentVelocity;
        rigidbody.useGravity = false;
        currentVelocity = rigidbody.velocity;

        dashParticle.GetComponent<ParticleSystem>().Play(false);
        switch (commend)
        {
            case 1:
                rigidbody.velocity = new Vector3(0, dashPower, 0);
                break;
            case 2:
                rigidbody.velocity = new Vector3(0, -dashPower, 0);
                break;
            case 3:
                rigidbody.velocity = new Vector3(-dashPower, 0, 0);
                break;
            case 4:
                rigidbody.velocity = new Vector3(dashPower, 0, 0);
                break;
        }
        yield return new WaitForSeconds(0.1f);

        dashParticle.GetComponent<ParticleSystem>().Stop(false);

        rigidbody.useGravity = true;
        rigidbody.velocity = currentVelocity;
        isDashNow = false;
    }

}
