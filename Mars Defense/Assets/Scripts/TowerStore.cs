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
    

    Currency currency;

    private void Start()
    {
        GameObject manager = GameObject.Find("Manager");
        currency = manager.GetComponent<Currency>();
    }

    //instantiate tower prebuild at the button's transform if
    //you can afford it
    public void InstantiatePrebuild()
    {


        if (currency.money >= cost) 
        {
            GameObject temp = new GameObject();

            Transform tempTransform = temp.transform;

            tempTransform.position = buttonTransform.position;

            //Debug.Log("Instantiating tower");
            Instantiate(tower1PrebuildPrefab, tempTransform);
            return;
     
        } else
        {
            Debug.Log("Not enough money to buy");
        }
    }
}
