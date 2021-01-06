using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            //destroys the turret prebuild if there is one
            try
            {
                GameObject prebuild = GameObject.FindGameObjectWithTag("Prebuild");
                Destroy(prebuild);
            }
            catch (Exception ex)
            {
                Debug.Log("Cannot find prebuild. Exception: " + ex.ToString());
            }
        }
    }

    public void EscapeFromGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void PlayGameAgain()
    {
        Debug.Log("Playing Game Again. Loading scene Main");
        SceneManager.LoadScene("Main");
    }
}
