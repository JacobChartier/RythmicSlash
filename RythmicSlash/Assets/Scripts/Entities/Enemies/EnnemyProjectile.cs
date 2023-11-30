using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<EnemyAI>().OnAttack += FireProjectile;
    }

    public void FireProjectile()
    {
        Vector3 direction = new Vector3(0, 0, 0);

        if (transform.localScale.x == -1)
            direction = new Vector3(0, 0, 180);

        Quaternion rotation = Quaternion.Euler(direction);

        Debug.Log($"<color=#FFFFFF>{gameObject.name}</color> fired a projectile!");

        Instantiate(projectilePrefab, projectileSpawnPoint.position, rotation);
    }

    private void OnDisable()
    {
        EnemyAI enemyAI = FindObjectOfType<EnemyAI>();
        if(enemyAI != null )
        {
            enemyAI.OnAttack -= FireProjectile;
        }
    }
}
