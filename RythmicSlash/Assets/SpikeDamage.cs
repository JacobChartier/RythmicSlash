using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.Mathematics;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{

    // Fonction appel�e lorsque quelque chose entre en collision avec le pique
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // V�rifie si l'objet en collision est le joueur
        if (collision.CompareTag("Player"))
        {
            // Appelle une fonction pour d�truire le joueur
            DestroyPlayer(collision.gameObject);
        }
    }

    // Fonction pour d�truire le joueur
    private void DestroyPlayer(GameObject player)
    {
        // D�truit le GameObject du joueur
        Destroy(player);

        // Vous pouvez ajouter d'autres actions en fonction de vos besoins.
        Debug.Log("Le joueur a �t� d�truit !");
    }
}
