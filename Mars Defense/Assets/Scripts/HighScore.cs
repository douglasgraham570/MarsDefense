using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HighScore : MonoBehaviour
{
    /***********************************/
    /*            VARIABLES            */
    /***********************************/
    [SerializeField] TextMeshProUGUI scoreGUI;
    [SerializeField] TextMeshProUGUI highScoreGUI;

    public int score = 0;
    int highScore = 0;
    
    /***********************************/
    /*              INIT               */
    /***********************************/

    void Awake()
    {
        
    }
    void Start()
    {
        //Load high score from player prefs
        try
        {
            highScore = PlayerPrefs.GetInt("highScore", 0);
            highScoreGUI.text = "HIGH SCORE: " + highScore;
        }
        catch (Exception ex)
        {
            Debug.Log("cannot load high score from player " +
                "prefs. Exception: " + ex);
        }
    }
   
    /***********************************/
    /*              LOOP               */
    /***********************************/

    void Update()
    {
        scoreGUI.text = "SCORE: " + score;

        if (score > highScore)
        {
            highScore = score;
            UpdateHighScore(highScore);
        }
    }

    /***********************************/
    /*             METHODS             */
    /***********************************/

    private void UpdateHighScore(int newScore)
    {
        Debug.Log("Updating high score");
        PlayerPrefs.SetInt("highScore", newScore);
        highScoreGUI.text = "HIGH SCORE: " + newScore;
    }
}
