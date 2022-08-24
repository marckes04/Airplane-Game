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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(Moving)
        {
            myBody.velocity = new Vector3(currentHorizontalSpeed, currentVerticalSpeed, myBody.velocity.z);
            myBody.transform.eulerAngles = currentAngle;
        }
    }

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

        foreach(var b in boxcolls)
        {
            b.enabled = true;
        }
    }
}
