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

    private void Start()
    {
        //get the sprite renderers for the base and all of the children
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //update tower's transform
        Vector3 pointerPosition = -Vector2.one;
        pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 1));
        transform.position = pointerPosition;
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        //determine if tower is placeable on this cell
        Vector3Int cellPosition = grid.WorldToCell(pos);
        TileBase tileBase = surface.GetTile(cellPosition);
        tile = tileBase.name;

        if (validTiles.Contains(tile))
        {
            placeable = true;
        } else
        {
            placeable = false;
        }
        Debug.Log(tile);

        //makes sure tower can only be placed below a certain y-value (to avoid overlapping with top panel)
        if (pointerPosition.y >= maxPlacementYVal)
        {
            placeable = false;
        }

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
            for(int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = new Color(1,0,0,.5f);
            }

        }
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
    }
}
