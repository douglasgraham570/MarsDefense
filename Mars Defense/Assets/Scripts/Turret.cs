using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //get reference to bullet
    public GameObject bulletPrefab;

    //time between firing
    public float timeBetweenFire = 1f;

    private float countdown = 0;
    private bool isEnemy = false;

    //initialize queue for enemy targets  
    private Queue<GameObject> targets = new Queue<GameObject>();

    private GameObject currentTarget;

    

    // Update is called once per frame
    void Update()
    {

        
        //fire if recharged and there is an enemy
        if (countdown <= 0 && isEnemy) 
        {
            Fire();
            countdown = timeBetweenFire;
        }

        countdown -= Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isEnemy = true;

        //Debug.Log("Enemy in range!");

        //enqueue enemy to targets
        targets.Enqueue(collision.gameObject);
        return;
    }



    void Fire()
    {
        Debug.Log("Firing!");

        //get reference to first target
        currentTarget = targets.Dequeue();
        Transform targetTransform = currentTarget.transform;

        //get vector from turrent to enemy
        Vector3 turretToEnemy = targetTransform.position - transform.position;

        //shoot target
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        //Debug.Log("Firing!");
    }
}
