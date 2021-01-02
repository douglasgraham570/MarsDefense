using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    //enemy movement speed along path
	public float speed = 1;

	public Transform[] waypoints;
    public float waypointRadius = 0.1f;

    public int moneyOnDeath = 1;
    public int livesTaken = 1;

    //reference to currency GUI
    private TextMeshProUGUI currencyText;

    Currency currency;
    Lives health;

    //current waypoint enemy is heading towards
    private Transform currentWaypoint;

    private int waypointIndex = 0;

    //Start is called before first frame
    void Start() {

        GameObject manager = GameObject.Find("Manager");
        currency = manager.GetComponent<Currency>();
        health = manager.GetComponent<Lives>();

        //get reference to the currency UGUI
        GameObject currencyObj = GameObject.FindGameObjectWithTag("Currency");
        currencyText = currencyObj.GetComponent<TextMeshProUGUI>();

        //get reference to the currency UGUI
        GameObject livesObj = GameObject.FindGameObjectWithTag("Lives");
        currencyText = livesObj.GetComponent<TextMeshProUGUI>();

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

            //destroy and take lives if we're at last waypoint
            if (waypointIndex >= waypoints.Length - 1)
            {
                //game over if the lives go to zero or lower
                if (health.lives - livesTaken <= 0)
                {
                    health.lives = 0;
                }
                else
                {
                    health.lives -= livesTaken;
                }

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
        if (collision.gameObject.tag == "Bullet")
        {
            currency.money += moneyOnDeath;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
