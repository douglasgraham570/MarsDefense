using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    Transform garbageTransform;

    private void Start()
    {
        garbageTransform = GetComponentInParent<Transform>();
    }

    void Update()
	{
        Collider2D[] colliders = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //if we have collided with the garbage can, so we should destroy the prebuild
        if (colliders.Length == 2)
        {
            garbageTransform.localScale = new Vector3(4.5f,4.5f,1);
        }
        else
        {
            garbageTransform.localScale = new Vector3(4f, 4f, 1);
        }

        //Debug.Log("Mouse position: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetKeyDown(KeyCode.Mouse0))
		{
            //Collider2D[] colliders = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //if we have collided with the garbage can, so we should destroy the prebuild
            if (colliders.Length == 2)
            {
                Collider2D colliderr = colliders[1];

                if (colliderr.name.Equals("Garbage"))
                {
                    GameObject prebuild = colliders[0].gameObject;
                    Destroy(prebuild);
                }
            }
        }
	}
}
