using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private GameObject pause;
    private bool isPaused = false;
    Canvas pauseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("Got a reference to the game over canvas ");

        pause = GameObject.FindGameObjectWithTag("PausePanel");
        pauseCanvas = pause.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPaused();
    }


    public void Pause()
    {
        isPaused = true;
    }

    private void CheckIfPaused()
    {
        if (Input.GetKey("escape") || isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;

            //pause canvas is visible now
            pauseCanvas.enabled = true; 
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game from pause");
        pauseCanvas.enabled = false;
        Time.timeScale = 1;
    }
}
