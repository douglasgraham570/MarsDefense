using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;

    public void Seek(Transform _target)
    {
        target = _target;

    }

    private void Update()
    {
        
        if (target == null)
        {
            Destroy(gameObject);
        }

    }
}
