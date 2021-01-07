using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public Wave[] waves;
    public int EnemiesAlive = 0;
    public int spawnNum = 0;
    public float timeBetweenEnemies = 0.5f;
    public float timeBetweenLevels = 10f;

    [SerializeField] Transform enemy1Prefab;
    [SerializeField] Transform enemy2Prefab;
    [SerializeField] Transform enemy3Prefab;
    [SerializeField] Transform spawnPoint;

    private float countdown = 2f; //when to spawn next wave
    private int waveIndex = 0;
    private bool gameStarted = false;
    private Transform currentPrefab;
    int a, b = 5;

    private void Awake()
    {
        //default enemy is enemy 1
        currentPrefab = enemy1Prefab;
    }

    //tells the script the game has started, and the enemies will now be spawned
    public void GameStarted()
    {
        Debug.Log("spawner knows game started");
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        //don't spawn new wave until we don't have enemies anymore
        if (gameStarted && (EnemiesAlive <= 0))
        {
            //spawn level when countdown reaches 0
            if (countdown <= 0f)
            {
                spawnNum += 1;

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

        for (int i = 0; i < wave.totalEnemies ; i++) //spawn whatever enemy has been selected
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        waveIndex++;
    }

    public void SpawnEnemy(Transform enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
