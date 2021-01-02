using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Lives : MonoBehaviour
{
    [Header("Initialization")]
    public int lives = 50;
    public TextMeshProUGUI livesUGUI;

    private GameObject gameOverCanvas;

    //continually update the lives due to enemies making it to HQ, or player making an upgrade/repair
    private void Update()
    {
        livesUGUI.text = "LIVES: " + lives.ToString();
    }

    
}

