using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    public GameObject towerMoverPrefab;

    private void OnMouseDown()
    {
        Debug.Log("On Mouse Down");
        Instantiate(towerMoverPrefab, transform);
    }
}
