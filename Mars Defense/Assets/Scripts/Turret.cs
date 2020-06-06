using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //get reference to bullet
    public GameObject bulletPrefab;

    //strength of firing
    public float strength = 5.0f;

    //adjusts firing spawn parameters
    public float up = 0f;
    public float down = 0f;
    public float left = 0f;
    public float right = 0f;


    //time between firing
    public float timeBetweenFire = 1f;

    //references to the turret tops at different angles
    Transform turretSE;
    Transform turretSW;
    Transform turretNW;
    Transform turretNE;

    private float countdown = 0;

    //initialize queue for enemy targets  
    private Queue<GameObject> targets = new Queue<GameObject>();

    private GameObject currentTarget;
    private Vector3 bulletSpawn;

    private void Start()
    {
        turretSE = transform.GetChild(1);
        turretSW = transform.GetChild(0);
        turretNW = transform.GetChild(3);
        turretNE = transform.GetChild(2);

    }

    // Update is called once per frame
    void Update()
    {
        //if enemy has already exited, remove it
        if (targets.Count != 0)
        {
            Debug.Log("Peeking at unequal queues");
            if (targets.Peek().name.Equals("empty"))
            {
                targets.Dequeue();
            }
        }

        //fire if recharged and there is an enemy
        if (countdown <= 0 && (targets.Count != 0)) 
        {
            Fire();
            countdown = timeBetweenFire;
        }

        countdown -= Time.deltaTime;

    }

    //enemy enters the turret's radius
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //enqueue enemy to targets

        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Enqueue(collision.gameObject);
            return;
        }
     
    }

    //enemy exits the turret's radius or is destroyed
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            //take enemy out from targets by briefly converting to array
            GameObject[] targetsArray = targets.ToArray();

            //set the exiting gameObject to null in the targets array so it can be ignored later
            for (int i = 0; i < targetsArray.Length; i++)
            {
                if (targetsArray.GetValue(i).Equals(collision.gameObject))
                {
                    targetsArray.SetValue(new GameObject("empty"), i);
                }
            }

            //reconstruct targets from the array
            targets = new Queue<GameObject>(targetsArray);
        }
    }


    void Fire()
    {
        //get reference to first target
        currentTarget = targets.Peek();
        Transform targetTransform = currentTarget.transform;

        //get vector from turrent to enemy
        Vector3 turretToEnemy = targetTransform.position - transform.position;

        //normalize vector
        turretToEnemy.Normalize();

        //rotate turret's head and instantiate bullet accordingly
        RotateAndShoot(turretToEnemy);
    }

    //given an enemy direction, rotates the turret to face that direction and instantiates bullet
    void RotateAndShoot(Vector3 enemyDir)
    {
        //get force to be applied to bullet
        Vector3 bulletForce = enemyDir * strength;

        //rotate turret and get intended bullet position
        if (enemyDir.x < 0 && enemyDir.y < 0)
        {
            //facing southwest
            MakeHeadVisible(turretSW, turretSE, turretNW, turretNE);
            bulletSpawn = transform.position + new Vector3(left, down, 0);

        } else if (enemyDir.x < 0)
        {
            //facing northwest
            MakeHeadVisible(turretNW, turretSW, turretSE, turretNE);
            bulletSpawn = transform.position + new Vector3(left, up, 0);
        }
        else if (enemyDir.y < 0)
        {
            //facing southeast
            MakeHeadVisible(turretSE, turretNW, turretNE, turretSW);
            bulletSpawn = transform.position + new Vector3(right, down, 0);
        }
        else
        {
            //facing northeast
            MakeHeadVisible(turretNE, turretNW, turretSW, turretSE);
            bulletSpawn = transform.position + new Vector3(right, up, 0);
        }

        //shoot target
        var bulletClone = Instantiate(bulletPrefab, bulletSpawn, Quaternion.identity);
        bulletClone.GetComponent<Rigidbody2D>().AddForce(bulletForce);
    }

    //given 4 turret heads, enables visibility for first head and disables it for rest 
    void MakeHeadVisible(Transform makeVisible, Transform hide1, Transform hide2, Transform hide3)
    {
        //make sprite renderer visible for this turret head
        var visibleRenderer = makeVisible.gameObject.GetComponent<SpriteRenderer>();
        visibleRenderer.enabled = true;

        //hide sprite renderer for these turret heads
        var invisibleRenderer1 = hide1.gameObject.GetComponent<SpriteRenderer>();
        invisibleRenderer1.enabled = false;

        var invisibleRenderer2 = hide2.gameObject.GetComponent<SpriteRenderer>();
        invisibleRenderer2.enabled = false;

        var invisibleRenderer3 = hide3.gameObject.GetComponent<SpriteRenderer>();
        invisibleRenderer3.enabled = false;
    }
}
