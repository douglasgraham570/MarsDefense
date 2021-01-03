using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerStore : MonoBehaviour
{
    public GameObject tower1PrebuildPrefab;
    public int cost = 5;
    public RectTransform buttonTransform;
    private int towerNumber = 1;

    public bool hasGameStarted = false;

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
    public void InstantiatePrebuild()
    {
        if (hasGameStarted)
        {
            if (currency.money >= cost)
            {
                GameObject temp = new GameObject();

                Transform tempTransform = temp.transform;

                tempTransform.position = buttonTransform.position;

                //Debug.Log("Instantiating tower");
                Instantiate(tower1PrebuildPrefab, tempTransform);
                return;

            }
            else
            {
                Debug.Log("Not enough money to buy");
            }
        }
        else
        {
            Debug.Log("Cannot buy. Game hasnot started yet!");
        }

    }
}
