using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] Transform enemy1Prefab;
    [SerializeField] Transform enemy2Prefab;
    [SerializeField] Transform enemy3Prefab;
    [SerializeField] Transform spawnPoint;

    public int spawnNum = 0;
    public float timeBetweenEnemies = 0.5f;
    public float timeBetweenLevels = 10f;

    private float countdown = 2f;
    private int levelIndex = 0;
    private bool gameStarted = false;
    private Transform currentPrefab;

    //serialize an enemy speed bar that designer can adjust

    //GameObject manager;
    //Enemy enemyScript;

    //tells the script the game has started, and the enemies will now be spawned
    public void GameStarted()
    {
        //manager = GameObject.FindGameObjectWithTag("Manager");
        //enemyScript = manager.

        Debug.Log("spawner knows game started");
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            //spawn level when countdown reaches 0
            if (countdown <= 0)
            {
                spawnNum += 1;

                //spawn the next level
                SpawnLevel();

                //reset countdown to the time between levels
                countdown = timeBetweenLevels;

            }


            //decremet countdown relative to framerate
            countdown -= Time.deltaTime;

        }
    }

    //spawns a level
    void SpawnLevel()
    {
        levelIndex += 1;

        //spawn wave of enemies
        StartCoroutine(Spawn());
   
    }

    //spawns a wave of a certain enemy type
    IEnumerator Spawn()
    {
        if (levelIndex < 5) //spawn enemy1
        {
            currentPrefab = enemy1Prefab;
        }
        else if (levelIndex == 5) //spawn enemy2
        {
            currentPrefab = enemy2Prefab;
            spawnNum = 1;
        }
        else if (levelIndex < 20) //spawn enemy2
        {
            currentPrefab = enemy2Prefab;
        }
        else if (levelIndex == 20)//spawn enemy3
        {
            currentPrefab = enemy3Prefab;
            spawnNum = 1;
        }
        else if (levelIndex == 10) //spawn enemy2
        {
            currentPrefab = enemy2Prefab;
        }

        for (int i = 0; i < spawnNum ; i++) //spawn whatever enemy has been selected
        {
            Instantiate(currentPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
            
    }
}
