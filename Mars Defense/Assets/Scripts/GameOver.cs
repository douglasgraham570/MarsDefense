using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public int startingLives = 1;
    public int startingCurrency = 100;

    GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);

        //set starting lives and currency
        Lives.lives = startingLives;
        Currency.money = startingCurrency;
    }

    // Update is called once per frame
    void Update()
    {
        //activate game over mode
        if (Lives.lives == 0)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }

        //quits game if escape key is pressed
        if (Input.GetKey("escape")) {
            //Debug.Log("exiting");
            Application.Quit();
        }
    }

    //restarts game when 'play again' button pressed
    public void OnPlayAgain()
    {
        //Debug.Log("Reseting scene");
        //Lives.lives = startingLives;
        //Currency.money = startingCurrency;
        //gameOverCanvas.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //quits application when exit button pressed
    public void OnExit() {

        //Debug.Log("exiting");
        Application.Quit();
    }
}
