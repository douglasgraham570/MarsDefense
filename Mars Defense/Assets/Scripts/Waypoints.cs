using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
	public static Transform[] waypoints;
    public Transform waypointParent;

    // Awake is called immediately upon loading
    void Awake()
    {
        //get reference to waypoints game object
        waypoints = new Transform[waypointParent.childCount];


        //loop through the waypoints and put them into the array
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
