using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaneScript : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player1");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < player.transform.position.z - 300)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1")
        {
            gameObject.SetActive(false);
        }
    }
}
