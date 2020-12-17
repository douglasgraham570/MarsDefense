using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    [Header("Initialization")]
    public TextMeshProUGUI currencyUGUI;

    public int money = 200;
    public int towerOneCost = 20;
    public int towerTwoCost = 50;
    public int towerThreeCost = 90;

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
