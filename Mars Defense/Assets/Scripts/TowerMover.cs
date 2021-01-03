using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerMover : MonoBehaviour
{
    public GameObject towerPrefab;
    public Tilemap surface;
    public Grid grid;
    public List<string> validTiles = new List<string>();
    public List<string> nonvalidTiles = new List<string>();
    public float maxPlacementYVal = 0;
    public int towerOneCost = 20;


    private GameObject towerLocation;
    private bool placeable = true;
    private string tile;
    private SpriteRenderer[] spriteRenderers;

    Vector3 pointerPosition = -Vector2.one;
    Vector3 pos;

    Currency currency;

    private void Start()
    {
        GameObject manager = GameObject.Find("Manager");
        currency = manager.GetComponent<Currency>();

        //get the sprite renderers for the base and all of the children
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("mouse position: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        MoveTowerToMousePosition();
        DetermineIfPlaceable();
        PossiblyChangeColor();
    }

    private void PossiblyChangeColor()
    {
        //display sprite renderers as red if not placeable
        if (placeable)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = new Color(1, 1, 1, .5f);
            }

        }
        else
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = new Color(1, 0, 0, .5f);
            }

        }
    }

    private void DetermineIfPlaceable()
    {
        Vector3Int cellPosition = grid.WorldToCell(pos);
        TileBase tileBase = surface.GetTile(cellPosition);
        tile = tileBase.name;


        if (validTiles.Contains(tile))
        {
            placeable = true;
        }
        else
        {
            placeable = false;
        }
        //Debug.Log(tile);

        //is there enough money to buy the tower?
        if (currency.money < towerOneCost)
        {
            placeable = false;
        }

        Debug.Log("pointer position y value: " + pointerPosition.y.ToString());

        //makes sure tower can only be placed below a certain y-value (to avoid overlapping with top panel)
        if (pointerPosition.y >= maxPlacementYVal)
        {
            Debug.Log("pointer position greater than placement val");
            placeable = false;
        }
    }

    private void MoveTowerToMousePosition()
    {
        Vector3 pointerPosition = -Vector2.one;
        pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 1));
        transform.position = pointerPosition;
        pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    //tower is placed or put back (into menu)
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
    }
}
