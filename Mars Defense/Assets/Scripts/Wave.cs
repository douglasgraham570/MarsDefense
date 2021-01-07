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
    public float spawnRate;
    public List<Transform> enemyOrder = new List<Transform>();

    GameObject manager;
    LevelSpawner spawner;


    /***********************************/
    /*              INIT               */
    /***********************************/

    public void Init()
    {
        manager = GameObject.Find("Manager");
        spawner = manager.GetComponent <LevelSpawner>();
    }

    /***********************************/
    /*             METHODS             */
    /***********************************/

    //initialize the enemyOrder array to the desired order of enemies arriving
    public void DetermineEnemyOrder()
    {
        //for every enemy selected
        for (int i = 0; i < enemies.Count; i++)
        {
            try
            {
                //add one enemy for each count of that enemy
                for (int j = 0; j < counts[i]; j++)
                {
                    foreach (var orderedEnemy in enemyOrder)
                    {
                        Debug.Log("enemy before ");
                    }
                    Debug.Log("count: " + j + " out of: " + counts[i]);

                    enemyOrder.Add(enemies[i]);
                    
                        Debug.Log("enemy order count " + enemyOrder.Count);
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        Debug.Log("size of enemyOrder after adding: " + enemyOrder.Count);

    }

    public void SpawnNextEnemy(int enemyIndex)
    {
        spawner.SpawnEnemy(enemyOrder[enemyIndex]);
    }
}
