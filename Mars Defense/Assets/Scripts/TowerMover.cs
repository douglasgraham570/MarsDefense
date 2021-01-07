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

    private GameObject towerLocation;
    private bool placeable = true;
    private string tile;
    private SpriteRenderer[] spriteRenderers;
    private Color originalColor;
    Vector3 pointerPosition = -Vector2.one;
    Vector3 pos;
    Currency currency;
    int towerCost = 0;

    private void Start()
    {
        GameObject manager = GameObject.Find("Manager");
        currency = manager.GetComponent<Currency>();

        //get the sprite renderers for the base and all of the children
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        //get reference to original color
        originalColor = GetComponent<SpriteRenderer>().color;

        Debug.Log("tower prefab tag: " + towerPrefab.tag);

        towerCost = currency.GetCost(towerPrefab.tag);
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
        //display color normally if placeable
        if (placeable)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = new Color(originalColor.r, originalColor.b, originalColor.g, .5f);
            }

        }
        else //display as red and transparent, but otherwise keep the original colors
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                Debug.Log("original RGB r value: " + originalColor.r.ToString());
                spriteRenderers[i].color = new Color(originalColor.r + .3f, originalColor.b, originalColor.g, .5f);
            }

        }
    }

    private void DetermineIfPlaceable()
    {
        Vector3Int cellPosition = grid.WorldToCell(pos);
        //Debug.Log("Cell position: " + cellPosition.ToString());
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
        if (currency.money < towerCost)
        {
            placeable = false;
            Debug.Log("Not enough money to buy!");
        }

        //Debug.Log("pointer position y value: " + pointerPosition.y.ToString());

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
        DetermineIfPlaceable();

        if (placeable && (currency.money >= towerCost))
        {
            currency.Purchase(towerPrefab.tag);
            Debug.Log("^*$%^&$%^& The tag of selected tower is: " + tag);

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
