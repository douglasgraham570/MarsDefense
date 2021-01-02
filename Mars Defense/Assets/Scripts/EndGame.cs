using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private Lives health;
    private GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Lives>();
        Debug.Log("Got a reference to the game over canvas ");

        gameOver = GameObject.FindGameObjectWithTag("GameOverPanel");
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (health.lives <= 0)
        {
            //game over canvas is visible now
            Canvas gameOverCanvas = gameOver.GetComponent<Canvas>();
            gameOverCanvas.enabled = true;

            //freezes time
            Time.timeScale = 0;
        }
    }
}
