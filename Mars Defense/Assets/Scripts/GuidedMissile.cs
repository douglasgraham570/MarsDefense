using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour
{
    Transform target;

    public float speed = 1f;

    public void Seek(Transform _target)
    {
        target = _target;

    }

    private void Update()
    {
        //destroy bullet if no target
        if (target == null)
        {
            Destroy(gameObject);    
        } else
        {

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            //move towards enemy
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }


    }
}
