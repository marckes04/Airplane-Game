using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    private GameObject player;

    [HideInInspector]
    public bool GameStarted;

    private bool CameraLockToPlayer;

    private Vector3 currentAngle;
    private int CameraType = 0;

    public Vector3 cameraMenuPosition = new Vector3(174f, 33f, -2314);
    public float cameraTilt = 10f;

    private bool cameraEndScreen = false;

    //Add Player Controller
    private PlayerController playerController;


    void Start()
    {
        currentAngle = transform.eulerAngles;
        transform.position = cameraMenuPosition;

        player = GameObject.FindWithTag("Player1");

        // get the player controller
        playerController = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        SwitchCameraType();
    }

    private void LateUpdate()
    {
      LerpCameraToGameStart();
        cameraFollowPlayer();
    }

    void LerpCameraToGameStart()
    {
        if (GameStarted)
        {
            transform.position = new Vector3(
          Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime / 1.5f),
          Mathf.Lerp(transform.position.y, player.transform.position.y + 20f, Time.deltaTime / 1.5f),
          Mathf.Lerp(transform.position.z, player.transform.position.z - 80f, Time.deltaTime / 1.5f)
          );

            currentAngle = new Vector3(
                Mathf.LerpAngle(currentAngle.x, cameraTilt, Time.deltaTime / 1.5f),
                Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime / 1.5f),
                Mathf.LerpAngle(currentAngle.z, 0f, Time.deltaTime));

            transform.eulerAngles = currentAngle;

            if (transform.position.x > player.transform.position.x - 0.5f)
            {
                GameStarted = false;
                CameraLockToPlayer = true;

                print("Camera Stop Lerping");

                // call player take off Animation
                playerController.startTakeOff();
            }
        }
    }

    void cameraFollowPlayer()
    {
        if (CameraLockToPlayer && !cameraEndScreen)
        {
            if (CameraType == 0)
            {
                transform.position = new Vector3(
                    Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime * 5f),
                    Mathf.Lerp(transform.position.y, player.transform.position.y + 20f, Time.deltaTime + 5f),
                   player.transform.position.z - 80f);

                transform.eulerAngles = new Vector3(cameraTilt, 0f, 0f);
            }
            else
            {
                transform.position = new Vector3(250f,90f,player.transform.position.z-120f);

                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
    }//Follow Player

    void SwitchCameraType()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CameraType == 0)
                CameraType = 1;
            
            else
             CameraType = 0;
        }
    }
}
