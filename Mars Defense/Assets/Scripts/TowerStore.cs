using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerStore : MonoBehaviour
{
    public GameObject turboPrefab;
    public GameObject bluePrefab;
    public GameObject shadowPrefab;
    public int turboCost = 20;
    public int redCost = 50;
    public int shadowCost = 100;
    public RectTransform buttonTransform;
    public bool hasGameStarted = false;

    private int cost = 0;
    private GameObject selectedPrefab;

    Currency currency;

    private void Start()
    {
        GameObject manager = GameObject.Find("Manager");
        currency = manager.GetComponent<Currency>();
    }

    public void TellStoreGameStarted()
    {
        hasGameStarted = true;
    }

    //instantiate tower prebuild at the button's transform if
    //you can afford it
    public void InstantiatePrebuild(string towerName)
    {
        if (hasGameStarted)
        {
            switch (towerName)
            {
                case "turbo":
                    cost = turboCost;
                    selectedPrefab = turboPrefab;
                    break;
                case "blue":
                    cost = redCost;
                    selectedPrefab = bluePrefab;
                    break;
                case "shadow":
                    cost = shadowCost;
                    selectedPrefab = shadowPrefab;
                    break;
                default:
                    cost = 0;
                    break;
            }

            if (currency.money >= cost)
            {
                GameObject temp = new GameObject();

                Transform tempTransform = temp.transform;

                tempTransform.position = buttonTransform.position;

                Debug.Log("Instantiating tower: " + selectedPrefab.name);
                Instantiate(selectedPrefab, tempTransform);
                return;

            }

                Debug.Log("Not enough money to buy this tower");
            
        }
        else
        {
            Debug.Log("Cannot buy tower. Game has not started yet!");
        }

    }
}
