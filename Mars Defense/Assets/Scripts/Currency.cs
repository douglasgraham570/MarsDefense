using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField] TextMeshProUGUI currencyUGUI;
    [SerializeField] TextMeshProUGUI turboUGUI;
    [SerializeField] TextMeshProUGUI blueUGUI;
    [SerializeField] TextMeshProUGUI shadowUGUI;

    public int money = 200;
    public int turboCost = 20;
    public int blueCost = 50;
    public int shadowCost = 90;

    //subtract money based on the tower purchased
    public void Purchase(string towerTag)
    {
        Debug.Log("########  Tag received: " + towerTag);
        switch (towerTag)
        {
            case "Turbo":
                money -= turboCost;
                break;
            case "Blue":
                money -= blueCost;
                Debug.Log("()()()() in Blue case. money being subtracted by: "
                    + blueCost);
                break;
            case "Shadow":
                money -= shadowCost;
                break;
            default:
                money -= turboCost;
                break;
        }
    }

    private void Start()
    {
        turboUGUI.text = "$" + turboCost.ToString();
        blueUGUI.text = "$" + blueCost.ToString();
        shadowUGUI.text = "$" + shadowCost.ToString();
    }

    //continually update the currency due to enemies hit, towers/upgrades bought, and levels finished
    private void Update()
    {
        currencyUGUI.text = "$" + money.ToString();
    }
}
