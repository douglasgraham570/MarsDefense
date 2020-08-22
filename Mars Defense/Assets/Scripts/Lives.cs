﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lives : MonoBehaviour
{
    [Header("Initialization")]
    public static int lives;
    public TextMeshProUGUI livesUGUI;


    //continually update the lives due to enemies making it to HQ, or player making an upgrade/repair
    private void Update()
    {
        livesUGUI.text = "LIVES: " + lives.ToString();
    }
}
