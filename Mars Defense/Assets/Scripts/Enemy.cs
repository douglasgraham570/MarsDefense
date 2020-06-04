using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //enemy movement speed along path
	public float speed = 1;

	public static Transform[] waypoints;
    public float waypointRadius = 0.1f;

    //current waypoint enemy is heading towards
    private Transform currentWaypoint;

    private int waypointIndex = 0;

    //Start is called before first frame
    void Start() {

        waypoints = Waypoints.waypoints;

        //set initial waypoint 
        currentWaypoint = waypoints[waypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        //get a direction vector for enemy movement
        Vector3 direction = currentWaypoint.position - transform.position;

        //if enemy reaches current waypoint, go to next one
        if (direction.magnitude <= waypointRadius)
        {
            waypointIndex += 1;

            //destroy if we're at last waypoint
            if (waypointIndex >= waypoints.Length - 1)
            {
                Destroy(gameObject);
                return;
            }

            currentWaypoint = waypoints[waypointIndex];
            direction = currentWaypoint.position - transform.position;
        }

        //move the enemy along the normalized movement vector
        Vector3 translation = direction.normalized;

        transform.Translate(translation* Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if it's a bullet, both it and the bullet are destroyed

        //Debug.Log("Triggered!");

        if (collision.gameObject.tag == "Bullet")
        { 
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
