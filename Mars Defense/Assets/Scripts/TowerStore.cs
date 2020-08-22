using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerStore : MonoBehaviour
{
    public GameObject tower1PrebuildPrefab;
    public RectTransform buttonTransform;

    //instantiate tower prebuild at the button's transform
    public void InstantiatePrebuild()
    {
        GameObject temp = new GameObject();

        Transform tempTransform = temp.transform;

        tempTransform.position = buttonTransform.position;

        //Debug.Log("Instantiating tower");
        Instantiate(tower1PrebuildPrefab, tempTransform);
        return;
    }
}
