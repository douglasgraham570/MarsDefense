﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    [Header("Initialization")]
    public static int money = 100;
    public TextMeshProUGUI currencyUGUI;

    public static bool Purchase(GameObject towerPrefab)
    {
        return true;
    }

    //continually update the currency due to enemies hit, towers/upgrades bought, and levels finished
    private void Update()
    {
        currencyUGUI.text = "$" + money.ToString();

    }
}
