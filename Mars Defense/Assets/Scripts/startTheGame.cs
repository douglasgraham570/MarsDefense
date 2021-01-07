using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTheGame : MonoBehaviour
{

    private GameObject startGameMenu;

    private void Awake()
    {
        startGameMenu = GameObject.FindGameObjectWithTag("GameStartPanel");
        Canvas gameStartCanvas = startGameMenu.GetComponent<Canvas>();
        gameStartCanvas.enabled = true;
    }
}
