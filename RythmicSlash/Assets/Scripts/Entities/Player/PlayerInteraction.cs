using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private int collectibleCollected = 0;

    public void InteractWithCollectible(GameObject gameObject)
    {
        Destroy(gameObject);
        collectibleCollected++;
    }
}
