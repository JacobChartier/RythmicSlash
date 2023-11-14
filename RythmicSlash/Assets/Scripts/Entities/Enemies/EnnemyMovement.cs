using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public float attackRadius = 2f;


    private void Start()
    {

    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer > attackRadius)
            {
                // Le joueur est détecté mais hors de portée d'attaque
                // L'ennemi se déplace vers le joueur

            }
            // Notez que vous pourriez ajouter ici la logique d'attaque
        }
        else
        {
            // Le joueur n'est pas dans le rayon de détection
            // L'ennemi retourne à sa position d'origine

        }
    }
}
