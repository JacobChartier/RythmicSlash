using System.Collections.Generic;
using UnityEngine;

public class FiringBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileFirePoint;

    [SerializeField] private GameObject projectileParent;
    [SerializeField] private int initialPoolSize = 2;
    [SerializeField] private Queue<GameObject> projectilesPool = new Queue<GameObject>();


    private void Awake()
    {
        projectileParent = new GameObject();
        projectileParent.name = "Projectile pool";

        AddProjectileToQueue(initialPoolSize);
    }

    private void AddProjectileToQueue(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.parent = projectileParent.transform;
            projectile.SetActive(false);

            projectilesPool.Enqueue(projectile);

            projectile.GetComponent<Projectile>().SetPoolReference(projectilesPool);
        }
    }

    public void FireProjectile()
    {
        Vector3 direction = new Vector3(0, 0, 0);

        if (transform.localScale.x == -1)
            direction = new Vector3(0, 0, 180);

        Quaternion rotation = Quaternion.Euler(direction);

        Debug.Log($"<color=#FFFFFF>{gameObject.name}</color> fired a projectile!");

        if (projectilesPool.Count <= 0)
        {
            AddProjectileToQueue(2);
        }

        GameObject projectile = projectilesPool.Dequeue();
        projectile.SetActive(true);
        projectile.transform.position = projectileFirePoint.position;
        projectile.transform.rotation = rotation;
    }
}
