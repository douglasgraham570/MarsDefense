﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilder : MonoBehaviour
{
    public GameObject towerMoverPrefab;
    
    private void OnMouseDown()
    {
        Debug.Log("Clicked turret");
    }
}
