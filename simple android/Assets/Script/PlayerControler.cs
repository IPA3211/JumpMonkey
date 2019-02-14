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
    isOnWall,
    isAlreadyPaused;

    public float lifeTime;
    private float lifeTimeCurrent;

    public GameObject system;

    public float xPower, yPower, DragSpeed;
    public float dashPower;

    public GameObject fireParticle, dashParticle;
    public GameObject getMoneyEffect;

    Vector3 currentVelocity;
    Vector3 currentVelocityForPause;

    private Rigidbody rigidbody;
    private SoundManager soundManager;
    private GameSystem gameSystem;

    private float rotate;

    // Use this for initialization
    void Start() {
        try
        {
            GameObject.Find("DontDestoryOnLoad").GetComponent<DataSystem>().SetCharactor(gameObject);
        }
        catch
        {
            Debug.Log("SetCharactorInfo failed");
        }

        Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true);
        rigidbody = gameObject.GetComponent<Rigidbody>();
        soundManager = system.GetComponent<SoundManager>();
        gameSystem = system.GetComponent<GameSystem>();

        rigidbody.velocity = new Vector3(-xPower, yPower);
        isJumpRight = !isJumpRight;
        isCanJump = false;

        lifeTimeCurrent = lifeTime;

        StartCoroutine("CycleScore");
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();


        if (!GameSystem.isPasued)
        {
            rigidbody.isKinematic = false;
            SyncPause();
            lifeTimeCurrent -= Time.deltaTime;

            if (lifeTimeCurrent > 0)
                InputCheck();

            if (rigidbody.velocity.y < -DragSpeed)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, -DragSpeed);
            }
        }
        else
        {
            SyncPause();
            rigidbody.isKinematic = true;
        }
    }

    private void InputCheck() {
        if ((Input.GetMouseButton(0) || Input.GetKey("space")) && isCanJump)
        {
            Jump();
        }

        if (!isDashNow && !isCanJump)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Money"))
        {
            OnMoneyHit(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy")) {

            OnEnemyHit();
        }

        rigidbody.velocity = new Vector3(0, 0);
        isCanJump = true;
        isCanDashUp = true;
        isCanDashDown = true;
        isCanDashLeft = true;
        isCanDashRight = true;
        isOnWall = true;
        
        fireParticle.GetComponent<ParticleSystem>().Play(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        fireParticle.GetComponent<ParticleSystem>().Stop(false);
    }

    protected override void OnSwipeUp()
    {
        if (!isDashNow && !isCanJump)
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
        if (!isDashNow && !isCanJump)
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
        if (!isDashNow && !isCanJump)
        {
            if (isCanDashLeft)
            {
                StartCoroutine(Dash(3));
                isCanDashLeft = false;
            }
        }
    }

    protected override void OnSwipeRight() {
        if (!isDashNow && !isCanJump)
        {
            if (isCanDashRight)
            {
                StartCoroutine(Dash(4));
                isCanDashRight = false;
            }
        }
    }
    public void Jump() {
        soundManager.PlayOneShot(SoundManager.Sound.Jump);

        if (isJumpRight)
        {
            rigidbody.velocity = new Vector3(xPower, yPower);
            isJumpRight = !isJumpRight;
            isCanJump = false;
            StartCoroutine(RotateChar(false));
        }
        else
        {
            rigidbody.velocity = new Vector3(-xPower, yPower);
            isJumpRight = !isJumpRight;
            isCanJump = false;
            StartCoroutine(RotateChar(true));
        }
    }

    public void OnEnemyHit() {
        soundManager.PlayOneShot(SoundManager.Sound.Dead);
        gameSystem.SetActiveDeadMenu(true);
        Destroy(gameObject);
    }

    public void OnMoneyHit(Collider other) {
        lifeTimeCurrent = lifeTime;
        soundManager.PlayOneShot(SoundManager.Sound.getMoney);
        gameSystem.AddScore(100);

        GameObject obj = Instantiate(getMoneyEffect, other.transform.position + new Vector3(0, 1, 0), other.transform.rotation);

        Destroy(obj, 1);
        other.gameObject.SetActive(false);

        system.GetComponent<MoneySpawn>().SpawnMoney();
    }

    private void SyncPause() {
        if (!isAlreadyPaused && GameSystem.isPasued) {
            currentVelocityForPause = rigidbody.velocity;
            isAlreadyPaused = true;
        }

        if (isAlreadyPaused && !GameSystem.isPasued) {
            rigidbody.velocity = currentVelocityForPause;
            isAlreadyPaused = false;
        }
    }

    IEnumerator CycleScore() {
        

        yield return new WaitForSeconds(1f);
        try
        {
            if (!GameSystem.isPasued)
                gameSystem.AddScore(10);
        }
        catch (System.Exception)
        {
        }
        StartCoroutine("CycleScore");
    }

    IEnumerator Dash(int commend)
    {
        soundManager.PlayOneShot(SoundManager.Sound.Dash);
        isDashNow = true;
        rigidbody.useGravity = false;
        if(rigidbody.velocity.x != 0)
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

        Debug.Log("Dash");
    }

    IEnumerator RotateChar(bool isRightTurn) {
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
