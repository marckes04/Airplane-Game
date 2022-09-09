using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animation anim;

    private Camera mainCamera;

    private Rigidbody myBody;

    public float forwardVelocity = 100f;
    public float minimumSpeed = -1500f;

    public float maxHorizontalSpeed = 250f;
    public float currentHorizontalSpeed = 0f;

    public float maxVerticalSpeed = 250f;
    public float currentVerticalSpeed = 0f;

    public float currentRotation = 0f;
    private Vector3 currentAngle;

    public float left_BorderLimitX = 130f;
    public float right_BorderLimitX = 370f;
    public float vertical_UpperLimit = 160f;
    public float vertical_LowerLimit = 40f;
    public float bonus_HorizontalSpeed = 0f;
    public float boost_HorizontalSpeed = 0f;

    public float startSpeed = 40f;

    [HideInInspector]
    public bool Moving = false;

    private Vector3 storedVelocity;


    private bool speed_Boosted = false;
    private int speed_Boost_Value = 100;
    private float speed_Boost_Timeout = 5f;
    private float speed_Boost_Timer = 0f;

    private EnemyPlaneSpawner enemyPlaneSpawner;
    private BallonSpawner ballonSpawner;
    private PickUpSpawner pickUpSpawner;

    private HUDController hudController;

    void Awake()
    {
        anim = GetComponent<Animation>();
        myBody = GetComponent<Rigidbody>();

        myBody.isKinematic = true;

        currentAngle = myBody.transform.eulerAngles;

        mainCamera = Camera.main;

        storedVelocity = new Vector3(0f, 0f, startSpeed);
    }
    void Start()
    {
        enemyPlaneSpawner = GameObject.Find("EnemyPlaneSpawner").GetComponent<EnemyPlaneSpawner>();
        ballonSpawner = GameObject.Find("AirBalloonSpawner").GetComponent<BallonSpawner>();
        pickUpSpawner = GameObject.Find("PickUpSpawner").GetComponent<PickUpSpawner>();
        hudController = GameObject.Find("HUD Controller").GetComponent<HUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBorderPlayers();
        CheckControlInput();
        KeyboardInput();
    }

    void FixedUpdate()
    {
        if (Moving)
        {
            myBody.velocity = new Vector3(currentHorizontalSpeed, currentVerticalSpeed, myBody.velocity.z);
            myBody.transform.eulerAngles = currentAngle;

            SpeedBoost();
        }
    }

    public void MoveRight()
    {
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, 0f, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, 70f, Time.deltaTime));

        currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed,
            maxHorizontalSpeed + bonus_HorizontalSpeed + boost_HorizontalSpeed, Time.deltaTime);
    }//Move Right

    public void MoveLeft()
    {
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, 0f, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, -70f, Time.deltaTime));

        currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed,
            -maxHorizontalSpeed + -bonus_HorizontalSpeed + -boost_HorizontalSpeed, Time.deltaTime);
    }//Move Left


    public void MoveUp()
    {
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, -35f, Time.deltaTime),
            currentAngle.y, currentAngle.z);

        currentVerticalSpeed = Mathf.Lerp(currentVerticalSpeed, maxVerticalSpeed, Time.deltaTime / 2f);

    }//Move Up

    public void MoveDown()
    {
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, 35f, Time.deltaTime),
            currentAngle.y, currentAngle.z);

        currentVerticalSpeed = Mathf.Lerp(currentVerticalSpeed, -maxVerticalSpeed, Time.deltaTime / 2f);

    }//Move Down

    void KeyboardInput()
    {
        if (Moving)
        {
            if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }

            if (Input.GetKey(KeyCode.W))
            {
                MoveUp();
            }

            if (Input.GetKey(KeyCode.S))
            {
                MoveDown();
            }
        }
    }

    void CheckControlInput()
    {
        if (Moving)
        {
            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, 0f, Time.deltaTime / 0.1f);
                currentAngle = new Vector3(
             Mathf.LerpAngle(currentAngle.x, currentAngle.x, Time.deltaTime),
             Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
             Mathf.LerpAngle(currentAngle.z, 0f, Time.deltaTime * 2f));
            }

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                currentVerticalSpeed = Mathf.Lerp(currentVerticalSpeed, 0f, Time.deltaTime / 0.1f);

                currentAngle = new Vector3(
             Mathf.LerpAngle(currentAngle.x, 0f, Time.deltaTime * 2f),
             Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
             Mathf.LerpAngle(currentAngle.z, currentAngle.z, Time.deltaTime));

            }
        }
    }

    void CheckBorderPlayers()
    {
        if (Moving)
        {
            if (transform.position.y > vertical_UpperLimit)
            {
                transform.position = new Vector3(transform.position.x, vertical_UpperLimit - 1,
                    transform.position.z);

                currentVerticalSpeed = 0;
                Input.ResetInputAxes();
            }

            if (transform.position.y < vertical_LowerLimit)
            {
                transform.position = new Vector3(transform.position.x, vertical_LowerLimit + 1,
                    transform.position.z);

                currentVerticalSpeed = 0;
                Input.ResetInputAxes();
            }

            if (transform.position.x < left_BorderLimitX)
            {
                transform.position = new Vector3(left_BorderLimitX + 1, transform.position.y,
                    transform.position.z);

                currentHorizontalSpeed = 0f;

                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, 0f, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.z, 0f, Time.deltaTime * 2f));

                Input.ResetInputAxes();

            }

            if (transform.position.x > right_BorderLimitX)
            {
                transform.position = new Vector3(right_BorderLimitX - 1, transform.position.y,
                    transform.position.z);

                currentHorizontalSpeed = 0f;

                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, 0f, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.z, 0f, Time.deltaTime * 2f));

                Input.ResetInputAxes();

            }


        }
    }//Player border

    public void startTakeOff()
    {
        anim.Play("TakeOff");
    }

    public void Resume()
    {
        myBody.velocity = storedVelocity;
        myBody.isKinematic = false;

        Moving = true;

        BoxCollider[] boxcolls = GetComponents<BoxCollider>();

        foreach (var b in boxcolls)
        {
            b.enabled = true;
        }

        enemyPlaneSpawner.StartSpawningPlanes();
        ballonSpawner.StartSpawningBalloons();
        pickUpSpawner.StartSpawningPickUps();
        hudController.ActivateHUD(true);
    }

    void SpeedBoost()
    {
        if (speed_Boosted)
        {
            speed_Boost_Timer += Time.deltaTime;

            if (speed_Boost_Timer < speed_Boost_Timeout)
            {
                myBody.AddRelativeForce(Vector3.forward * speed_Boost_Value);
            }
            else
            {
                speed_Boosted = false;
                speed_Boost_Timer = 0f;

                myBody.velocity = storedVelocity;
                myBody.isKinematic = false;
                //Resume();
            }
        }
    }

    public void PlayerCrashed()
    {
        speed_Boosted = false;

        myBody.useGravity = true;
        myBody.mass = 2;
        myBody.transform.Rotate(0.2f, 0.2f, 0.2f);

        GetComponent<PlayerController>().enabled = false;
        StartCoroutine(PlayerDiedRestart());
    }

     IEnumerator PlayerDiedRestart()
    {
        yield return new WaitForSeconds(3f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fuel") {
            hudController.FuelCollected();
        }
        if (other.tag == "ScoreMultiplier") {
            hudController.IncreaseScore();
        }
        if (other.tag == "SpeedBoost") {
            speed_Boosted = true;
        }
        if (other.tag == "enemyAir") {
            PlayerCrashed();
        }
    }
}
