using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    private List<GameObject> islandPool = new List<GameObject>();
    private List<GameObject> enemyPlanePool = new List<GameObject>();
    private List<GameObject> cloudsPool = new List<GameObject>();
    private List<GameObject> pickUpPool = new List<GameObject>();
    private List<GameObject> ballonPool = new List<GameObject>();


    void Awake()
    {
        if (instance == null)
            instance = this;

        object[] list = Resources.LoadAll("Islands");

        for(int i=0; i< list.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject obj = (GameObject)list[i];
                obj.SetActive(false);
                islandPool.Add(Instantiate(obj));  
            }
        }  // Create Islands

        //Create Clouds
       list = Resources.LoadAll("Clouds");

        for (int i = 0; i < list.Length; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                GameObject obj = (GameObject)list[i];
                obj.SetActive(false);
                cloudsPool.Add(Instantiate(obj));
            }
        }

        //Create Planes
        list = Resources.LoadAll("EnemyAir");

        for (int i = 0; i < list.Length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject obj = (GameObject)list[i];
                obj.SetActive(false);
                enemyPlanePool.Add(Instantiate(obj));
            }
        }// Create Planes

        //Create Balloons
        list = Resources.LoadAll("Balloons");

        for (int i = 0; i < list.Length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject obj = (GameObject)list[i];
                obj.SetActive(false);
                ballonPool.Add(Instantiate(obj));
            }
        }// Create Ballons

        //Create Pickups
        list = Resources.LoadAll("PickUps");

        for (int i = 0; i < list.Length; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject obj = (GameObject)list[i];
                obj.SetActive(false);
                pickUpPool.Add(Instantiate(obj));
            }
        }// Create Ballons

    }//Awake

    public GameObject GetPooledIsland(string islandName)
    {
        GameObject pooledIsland = islandPool.Find(i => i.name == islandName && !i.activeInHierarchy);

        return pooledIsland;
    }

    public GameObject GetRandomPooledCloud()
    {
        int cloudChoice = Random.Range(0, cloudsPool.Count);

        while (cloudsPool[cloudChoice].activeInHierarchy)
        {
             cloudChoice = Random.Range(0, cloudsPool.Count);
        }

        return cloudsPool[cloudChoice];
    }

    public GameObject GetPooledEnemyPlane(string enemyPlaneName)
    {
        GameObject plane = enemyPlanePool.Find(p => p.name == enemyPlaneName && p.activeInHierarchy);
        return plane;
    }

    public List<GameObject> GetPooledEnemyPlanePool()
    {
        return enemyPlanePool;
    }

    public GameObject GetPooledBallon()
    {
        GameObject balloon = ballonPool.Find(b => !b.activeInHierarchy);

        return balloon;
    }

    public GameObject GetPooledPickUp(string PickUpName)
    {
        GameObject pickUp = pickUpPool.Find(p => p.name == PickUpName && !p.activeInHierarchy);

        return pickUp;
    }

}//Class
