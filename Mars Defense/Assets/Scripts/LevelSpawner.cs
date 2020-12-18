using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public int spawnNum = 0;
    public float timeBetweenEnemies = 0.25f;

    public float timeBetweenLevels = 5f;
    private float countdown = 2f;
    private int levelIndex = 0;
    private bool gameStarted = false;


    //tells the script the game has started, and the enemies will now be spawned
    public void GameStarted()
    {
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
        
        for (int i = 0; i < spawnNum ; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
            
    }
}
