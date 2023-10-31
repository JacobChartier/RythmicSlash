using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private int mouvementSpeed = 3;
    [SerializeField] private int maxJumps = 2, currentJumps = 0;
    [SerializeField] private float jumpForce = 12.5f;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        IsPlayerOnGround();
    }

    public void Move(int direction)
    {
        player.velocity = new Vector2((mouvementSpeed * direction), player.velocity.y);
    }

    public void Jump()
    {
        player.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private bool IsPlayerOnGround()
    {
        if (player.velocity.y < 0.05 || player.velocity.y > 0.05)
            return true;
        else
            return false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        
    }
}
