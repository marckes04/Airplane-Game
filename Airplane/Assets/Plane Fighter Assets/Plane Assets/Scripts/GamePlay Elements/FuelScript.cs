using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelScript : MonoBehaviour
{
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player1");
    }

    void Update()
    {
        transform.Rotate(0f, 0f, 0.7f);

        if(transform.position.z < player.transform.position.z - 100)
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
