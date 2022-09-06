using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonSpawner : MonoBehaviour
{
    private GameObject player;

    private bool continueSpawning;

    private WaitForSeconds waitforSeconds = new WaitForSeconds(10f);

    void Start()
    {
        player = GameObject.FindWithTag("Player1");
    }

  IEnumerator SpawnBallon()
    {
        while (continueSpawning)
        {
            GameObject balloon = Spawner.instance.GetPooledBalloon();
            balloon.SetActive(true);

            if(player.transform.position.y > 120)
            {
                balloon.transform.position = new Vector3(player.transform.position.x, -18, player.transform.position.z
                    + 200);
            }

            else
            {
                balloon.transform.position = new Vector3(player.transform.position.x, -18, player.transform.position.z
                   + 150);
            }

            yield return waitforSeconds;
        }
  }

    public void startSpawningBalloons()
    {
        continueSpawning = true;
        StartCoroutine(SpawnBallon());
    }

    public void stopSpawningBalloons()
    {
        continueSpawning = false;
        StopCoroutine(SpawnBallon());
    }

}
