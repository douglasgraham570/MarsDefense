using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    /***********************************/
    /*            VARIABLES            */
    /***********************************/
    public List<Transform> enemies;
    public List<int> counts;
    public int totalEnemies = 0;
    public float spawnRate;

    GameObject manager;
    LevelSpawner spawner;

    List<Transform> enemyOrder = new List<Transform>();

    /***********************************/
    /*              INIT               */
    /***********************************/

    void Awake()
    {
        //calculate total enemies
        foreach (var count in counts)
        {
            totalEnemies += count;
        }

        Debug.Log("Total enemies in the level: " + totalEnemies);

        manager = GameObject.Find("Manager");
        spawner = manager.GetComponent <LevelSpawner>();
    }

    void Start()
    {
        DetermineEnemyOrder();
    }

    /***********************************/
    /*             METHODS             */
    /***********************************/

    //initialize the enemyOrder array to the desired order of enemies arriving
    private void DetermineEnemyOrder()
    {
        //for every enemy selected
        for (int i = 0; i < enemies.Capacity; i++)
        {
            try
            {
                //add one enemy for each count of that enemy
                for (int j = 0; j < counts[i]; j++)
                {
                    enemyOrder.Add(enemies[i]);
                }

            }
            catch (Exception ex)
            {
                Debug.Log("counts list cannot have size less than enemies list");
            }
        }
    }

    public void SpawnNextEnemy(int enemyIndex)
    {
        spawner.SpawnEnemy(enemyOrder[enemyIndex]);
    }
}
