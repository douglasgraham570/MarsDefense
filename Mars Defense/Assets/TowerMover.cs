using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMover : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject towerLocation;

    // Update is called once per frame
    void Update()
    {
        Vector3 pointerPosition = -Vector2.one;

        pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 1f));
        transform.position = pointerPosition;
    }

    private void OnMouseDown()
    {
        towerLocation = new GameObject(towerPrefab.name);
        towerLocation.transform.position = transform.position;

        //instantiate the permanent gameObject
        Instantiate(towerPrefab, towerLocation.transform);

        //Destroy the mover temporary object
        Destroy(gameObject);
    }
}
