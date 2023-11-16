using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileFirePoint;

    public void FireProjectile()
    {
        Vector3 direction = new Vector3(0, 0, 0);

        if (transform.localScale.x == -1)
            direction = new Vector3(0, 0, 180);

        Quaternion rotation = Quaternion.Euler(direction);

        Debug.Log($"<color=#FFFFFF>{gameObject.name}</color> fired a projectile!");

        Instantiate(projectilePrefab, projectileFirePoint.position, rotation);
    }
}
