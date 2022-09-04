using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player1");

        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-15, -10), 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < player.transform.position.z)
            gameObject.SetActive(false);
    }
}
