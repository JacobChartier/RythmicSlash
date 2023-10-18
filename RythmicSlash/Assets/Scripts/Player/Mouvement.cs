using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private int mouvementSpeed = 3;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(int direction)
    {
        rigidbody2D.velocity = new Vector2((mouvementSpeed * direction), rigidbody2D.velocity.y);
    }
}
