using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHealth : MonoBehaviour
{
    [SerializeField] private int degats = 11;
    private void OnTriggerEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health)
        {
            Debug.Log("Collision avec SPIKE");
            health.TakeDamage(degats);
        }
    }

}
