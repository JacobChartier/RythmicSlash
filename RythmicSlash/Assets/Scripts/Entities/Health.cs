using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int MAX_HEALTH = 10;
    [SerializeField] private int currentHealth { get; set; }

    void Awake()
    {
        currentHealth = MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
