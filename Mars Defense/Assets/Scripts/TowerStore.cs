using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerStore : MonoBehaviour
{
    public GameObject turboPrefab;
    public GameObject bluePrefab;
    public GameObject shadowPrefab;
    public RectTransform buttonTransform;
    public bool hasGameStarted = false;

    GameObject selectedPrefab;

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
                    selectedPrefab = turboPrefab;
                    break;
                case "blue":
                    selectedPrefab = bluePrefab;
                    break;
                case "shadow":
                    selectedPrefab = shadowPrefab;
                    break;
                default:
                    break;
            }

            GameObject temp = new GameObject();

            Transform tempTransform = temp.transform;

            tempTransform.position = buttonTransform.position;

            Debug.Log("Instantiating tower: " + selectedPrefab.name);
            GameObject instantiatedTower = Instantiate(selectedPrefab, tempTransform);
            return;  
        }
    }
}
