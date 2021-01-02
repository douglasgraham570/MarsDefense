using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
	void Update()
	{
        //Debug.Log("Mouse position: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetKeyDown(KeyCode.Mouse0))
		{
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //         RaycastHit[] hits = Physics.RaycastAll(ray);
            Collider2D[] colliders = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //Debug.Log("Number of colliders hit: " + colliders.Length);

            //Debug.Log("Collider 0 hit: " + colliders[0].name);
            //Debug.Log("Collider 1 hit: " + colliders[1].name);

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
