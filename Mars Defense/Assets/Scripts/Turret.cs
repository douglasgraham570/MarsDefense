using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
     [Header("Attributes")]

    //time between firing
    public float timeBetweenFire = 1f;

    //time to update target
    public float targetUpdateTime = .5f;

    public bool hasGuidedMissiles = false;

    //strength of firing
    public float strength = 5.0f;

    //cost
    public int turretCost = 20;

    [Header("Prefabs")]

    public GameObject bulletPrefab;
    public GameObject missilePrefab;

    [Header("Settings")]
    public string enemyTag = "Enemy";

    //adjusts firing spawn parameters
    public float up = 0f;
    public float down = 0f;
    public float left = 0f;
    public float right = 0f;

    //references to the turret tops at different angles
    Transform turretSE;
    Transform turretSW;
    Transform turretNW;
    Transform turretNE;

    private float countdown = 0;

    //initialize queue for enemy targets  
    //private Queue<GameObject> targets = new Queue<GameObject>();

    private GameObject currentTarget = null;
    private Vector3 bulletSpawn;
    private float range;

    Currency currency;

    private void Awake()
    {

        GameObject manager = GameObject.Find("Manager");
        currency = manager.GetComponent<Currency>();


        //Debug.Log("Awaking Turret");
        currency.money -= turretCost;

        //get reference to each potential turret head
        turretSE = transform.GetChild(1);
        turretSW = transform.GetChild(0);
        turretNW = transform.GetChild(3);
        turretNE = transform.GetChild(2);

        //get turret radius from collider
        range = GetComponent<CircleCollider2D>().radius;
             
        //Repeatedly update the target enemy
        InvokeRepeating("UpdateTarget", 0f, targetUpdateTime);
        
    }
     
    void UpdateTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestTarget = null;
        float minDistance = Mathf.Infinity;

        //find the target with the lowest distance to the turret
        foreach (var target in targets)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);

            if (target != null && dist <= minDistance)
            {
                minDistance = dist;
                nearestTarget = target;
            } 
        }

        //if there is a target in the range, make it the current target
        if (nearestTarget != null && minDistance < range)
        {
            currentTarget = nearestTarget;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if  no enemies, do nothing
        if (currentTarget == null)
        {
            return;
        }

        //fire if recharged and there is an enemy
        if (countdown <= 0) 
        {
            Fire();
            currentTarget = null;
            countdown = timeBetweenFire;
        }

        countdown -= Time.deltaTime;

    }

    void Fire()
    {
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

        Vector3 shootingDir = currentTarget.transform.position - bulletSpawn;

        //get force to be applied to bullet
        Vector3 bulletForce = shootingDir * strength;

        //instantiate guided missile if upgraded
        if (hasGuidedMissiles)
        {
            var missileClone = Instantiate(missilePrefab, bulletSpawn, Quaternion.identity);

            //get reference to missile's script
			GuidedMissile missile = missileClone.GetComponent<GuidedMissile>();

			if (missile != null)
			{
				missile.Seek(currentTarget.transform);
			}
            
        }
        else
        {
            //otherwise instantiate bullet
            var bulletClone = Instantiate(bulletPrefab, bulletSpawn, Quaternion.identity);
            bulletClone.GetComponent<Rigidbody2D>().AddForce(bulletForce);

            //get reference to bullet's script
            Bullet bullet = bulletClone.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Seek(currentTarget.transform);
            }

        }
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
