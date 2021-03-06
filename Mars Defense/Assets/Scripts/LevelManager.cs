﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameStartCanvas;
    public GameObject gameOverCanvas;
    public int startingLives = 50;
    public int startingCurrency = 100;

    Currency currency;
    Lives health;


    GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
         //get a reference to the currency script attached to the manager
        GameObject manager = GameObject.Find("Manager");
        Debug.Log("is manager null? " + manager.ToString());
        currency = manager.GetComponent<Currency>();
        Debug.Log("is currency script null? " + currency.ToString());

        health = manager.GetComponent<Lives>();

        //first time playing through
        if (!PlayerPrefs.HasKey("started"))
        {
            gameStartCanvas.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            gameStartCanvas.SetActive(false);
        }

        
        gameOverCanvas.SetActive(false);

        //set starting lives and currency
        health.lives = startingLives;
        currency.money = startingCurrency;
    }

    // Update is called once per frame
    void Update()
    {
        //activate game over mode
        if (health.lives == 0)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0f;
        }

        //quits game if escape key is pressed
        if (Input.GetKey("escape")) {
            //Debug.Log("exiting");
            Application.Quit();
            PlayerPrefs.DeleteAll();
        }
    }

    public void OnPlay()
    {
        gameStartCanvas.SetActive(false);
        PlayerPrefs.SetInt("started", 1);
        Time.timeScale = 1f;
    }

    //restarts game when 'play again' button pressed
    public void OnPlayAgain()
    {
        //Debug.Log("Reseting scene");
        //Lives.lives = startingLives;
        //Currency.money = startingCurrency;
        //gameOverCanvas.SetActive(false);
        Time.timeScale = 1f;
        health.lives = startingLives;
        currency.money = startingCurrency;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    //quits application when exit button pressed
    public void OnExit() {

        //Debug.Log("exiting");
        Application.Quit();
        PlayerPrefs.DeleteAll();
    }

}
