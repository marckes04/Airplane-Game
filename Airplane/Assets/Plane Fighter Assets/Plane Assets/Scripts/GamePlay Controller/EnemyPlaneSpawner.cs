using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaneSpawner : MonoBehaviour
{
    private GameObject player;

    private float leftBorderLimitX;
    private float rightBorderLimitX;
    private float vertical_LowerLimit;
    private float vertical_UpperLimit;

    public float planeYSpawnPos = 90f;

    private bool continueSpawning;

    public static float waitTime = 10f;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(waitTime);

    void Start()
    {
        player = GameObject.FindWithTag("Player1");

        PlayerController p = player.GetComponent<PlayerController>();

        leftBorderLimitX = p.left_BorderLimitX;
        rightBorderLimitX = p.right_BorderLimitX;
        vertical_LowerLimit = p.vertical_LowerLimit;
        vertical_UpperLimit = p.vertical_UpperLimit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
