using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    private GameObject player;
    private bool continueSpawning;

    private Vector3 SpawnPosition;

    private WaitForSeconds waitseconds = new WaitForSeconds(4.5f);

    private void Awake()
    {
        player = GameObject.FindWithTag("Player1");
    }

    void Spawn_Speed_Boosters(Vector3 spawnPos)
    {
        GameObject speedBooster = Spawner.instance.GetPooledPickUp("SpeedBooster(Clone)");
        speedBooster.SetActive(true);

        speedBooster.transform.position = spawnPos;
    }

    void Spawn_Score_Boosters(Vector3 spawnPos)
    {
        GameObject scoreBooster = Spawner.instance.GetPooledPickUp("ScoreBooster(Clone)");
        scoreBooster.SetActive(true);

        scoreBooster.transform.position = spawnPos;
    }

    void Spawn_Score_Multiplier(Vector3 spawnPos)
    {
        GameObject scoreMultiplier = Spawner.instance.GetPooledPickUp("BlueScoreMultiplier(Clone)");
        scoreMultiplier.SetActive(true);

        scoreMultiplier.transform.position = spawnPos;
    }

    void SpawnFuel(Vector3 spawnPos)
    {
        GameObject fuel = Spawner.instance.GetPooledPickUp("Fuel(Clone)");
        fuel.SetActive(true);

        fuel.transform.position = spawnPos;
    }

    IEnumerator SpawnPickUp()
    {
        while (continueSpawning)
        {
            SpawnPosition = new Vector3(Random.Range(130, 250), Random.Range(50, 150),
                player.transform.position.z + 600);

            int typeOfPickUp = Random.Range(0, 8);

            if(typeOfPickUp == 0 || typeOfPickUp == 1 || typeOfPickUp == 2)
            {
                Spawn_Score_Boosters(SpawnPosition);
            }

            if (typeOfPickUp == 3 || typeOfPickUp == 4 )
            {
                Spawn_Score_Multiplier(SpawnPosition);
            }

            if (typeOfPickUp == 5 || typeOfPickUp == 6)
            {
                Spawn_Speed_Boosters(SpawnPosition);
            }
            if (typeOfPickUp == 7)
            {
                SpawnFuel(SpawnPosition);
            }

            yield return waitseconds;
        }
    }


    public void StartSpawningPickUps()
    {
        continueSpawning = true;
        StartCoroutine(SpawnPickUp());
    }

    public void StopSpawningPickUps()
    {
        continueSpawning = false;
        StopCoroutine(SpawnPickUp());
    }

}
