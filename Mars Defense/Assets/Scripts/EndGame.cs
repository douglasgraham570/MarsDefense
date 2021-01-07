using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameOverTitle;
    [SerializeField] TextMeshProUGUI gameOverScore;
    [SerializeField] HighScore highScore;

    private Lives health;
    private GameObject gameOver;
    Canvas gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Lives>();
        gameOver = GameObject.FindGameObjectWithTag("GameOverPanel");
        //game over canvas is visible now
        gameOverCanvas = gameOver.GetComponent<Canvas>();
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
            GameOver();
        }
    }

    //end the game
    private void GameOver() {

        //display score and highScore on the game over panel
        gameOverScore.text = "Score: " + highScore.score +
                        "\n" + "High Score: " + PlayerPrefs.GetInt("highScore");

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

    public void DisplayVictory()
    {
        gameOverTitle.text = "VICTORY!";
        GameOver();
    }
}
