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
    public Dictionary<GameObject, int> enemyCounts;
    public int totalEnemies = 0;
    public float spawnRate;

    GameObject manager;
    LevelSpawner spawner;

    /***********************************/
    /*              INIT               */
    /***********************************/

    void Awake()
    {
        //calculate total enemies
        foreach (var enemy in enemyCounts)
        {
            totalEnemies += enemy.Value;
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
        throw new NotImplementedException();
    }

    void SpawnNextEnemy()
    {
        //spawner.SpawnEnemy();
    }
}
