using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public Wave[] waves;
    public int EnemiesAlive = 0;
    public float timeBetweenLevels = 10f;

    [SerializeField] Transform spawnPoint;

    private float countdown = 2f; //when to spawn next wave
    private int waveIndex = 0;
    private bool gameStarted = false;
    private Transform currentPrefab;

    //tells the script the game has started, and the enemies will now be spawned
    public void GameStarted()
    {
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        //don't spawn new wave until we don't have enemies anymore
        if (gameStarted && (EnemiesAlive <= 0) && waveIndex < waves.Length)
        {
            //spawn level when countdown reaches 0
            if (countdown <= 0f)
            {
                StartCoroutine(Spawn());

                //reset countdown to the time between levels
                countdown = timeBetweenLevels;

                return;
            }

            //decremet countdown relative to framerate
            countdown -= Time.deltaTime;
        }
    }

    //spawns a wave of a certain enemy type
    IEnumerator Spawn()
    {
        Wave wave = waves[waveIndex];
        int waveEnemies = 0;

        foreach (var count in wave.counts)
        {
            waveEnemies += count;
        }

        Debug.Log("wave total enemies: " + waveEnemies + ". Should be 3");

        wave.Init();
        wave.DetermineEnemyOrder();

        Debug.Log("wave order: " + wave.enemyOrder.ToString());

        //TODO Fix wave total enemies
        for (int i = 0; i < waveEnemies ; i++) //spawn whatever enemy has been selected
        {
            //SpawnEnemy();
            wave.SpawnNextEnemy(i);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        waveIndex++;
    }

    public void SpawnEnemy(Transform enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
