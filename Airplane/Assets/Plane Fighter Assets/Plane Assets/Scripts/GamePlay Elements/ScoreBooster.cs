using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBooster : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player1");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 0.7f);

        if (transform.position.z < player.transform.position.z - 100)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            gameObject.SetActive(false);
        }
    }
}
