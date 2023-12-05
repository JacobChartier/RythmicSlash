using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyHealth : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;

    public void ResetEnemy()
    {
        Health health = GetComponent<Health>();

        health.currentHealth = 10;
        this.transform.position = SpawnPoint.position;
        this.gameObject.SetActive(true);
    }
}
