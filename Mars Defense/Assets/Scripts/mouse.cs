using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    public GameObject towerMoverPrefab;

    void OnMouseDown()
    {
        Debug.Log("On Mouse Down");
        Instantiate(towerMoverPrefab, transform);
    }
}
