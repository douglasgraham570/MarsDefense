using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    private void OnMouseOver()
    {
        //Changes color when mouse hovers over it
    }

    private void OnMouseDown()
    {
        //If mouse is holding a tower, destroy it (in same way as end game does)
        //destroys the turret prebuild if there is one

        Debug.Log("Mouse down over the garbage can");
        //try
        //{
        //    GameObject prebuild = GameObject.FindGameObjectWithTag("Prebuild");
        //    Destroy(prebuild);
        //}
        //catch (Exception ex)
        //{
        //    Debug.Log("Cannot find prebuild. Exception: " + ex.ToString());
        //}
    }
}
