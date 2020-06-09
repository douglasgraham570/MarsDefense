using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerMover : MonoBehaviour
{
    public GameObject towerPrefab;
    public Tilemap surface;
    private GameObject towerLocation;
    private bool placeable = true;
   

    // Update is called once per frame
    void Update()
    {
        //update tower's transform
        Vector3 pointerPosition = -Vector2.one;
        pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition+ new Vector3(0, 0, 1));
        transform.position = pointerPosition;

        Vector3Int cellPos = surface.LocalToCell(transform.position);
        string tileName = "Bob ross";
        Debug.Log(tileName);

    }

    private void OnMouseDown()
    {
        if (placeable)
        {

            towerLocation = new GameObject(towerPrefab.name);
            Transform towerTransform = towerLocation.transform;
            Vector3 towerPosition = towerTransform.position;

            //assign the prebuild transform's position to the tower's position
            towerPosition = transform.position;
            towerPosition.z = 0;

            //instantiate the permanent gameObject
            Instantiate(towerPrefab, towerPosition, Quaternion.identity);

            //Destroy the mover temporary object
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Cannot place tower here");
        }
    }
}
