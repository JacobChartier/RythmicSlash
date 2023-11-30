using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float detectionRadius = 35f;
    public float attackRadius = 25f;
    private float moveSpeed = 3f;
    private float chaseSpeed = 6f;
    public float attackCooldown = 3f;
    public float jumpForce = 0.2f;

    private Rigidbody2D enemyRb;
    private Vector3 originalPosition;
    private bool isPlayerDetected = false;

    public delegate void AttackEventHandler();
    public event AttackEventHandler OnAttack;
    private float lastAttackTime;
    private EnemyState currentState = EnemyState.Normal;

    private enum EnemyState
    {
        Normal, //Patrol
        MoveToPlayer, //Chase
        Attack,
        ReturnToOriginal,
    }

    private void Start()
    {
        originalPosition = transform.position;
        lastAttackTime = -attackCooldown;
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Normal:
                NormalBehavior();
                break;
            case EnemyState.MoveToPlayer:
                
                break;
            case EnemyState.Attack:
                AttackPlayer();
                break;
            case EnemyState.ReturnToOriginal:
                ReturnToOriginalPosition();
                break;

        }
    }

    void NormalBehavior()
    {
        // Vérifier la distance au joueur
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < detectionRadius)
        {
            // Le joueur est détecté
            isPlayerDetected = true;
            currentState = EnemyState.MoveToPlayer;
        }
        else if (!isPlayerDetected)
        {
            // Déplacer l'ennemi entre sa position d'origine et un point légèrement décalé
            Vector2 PatrolPosition = originalPosition + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0);
            Vector2 direction = (PatrolPosition -(Vector2)transform.position).normalized;
            enemyRb.velocity = direction * moveSpeed;

            smallJumpMovement();
        }
    }

    void MoveToPlayer()
    {

        // Déplacement vers le joueur
        Vector2 direction = (player.position - transform.position).normalized;
        enemyRb.velocity = direction * chaseSpeed;

        // Vérifier si l'ennemi est assez proche pour attaquer
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < attackRadius)
        {
            currentState = EnemyState.Attack;
        }
        if (distanceToPlayer > detectionRadius || transform.position == originalPosition)
        {
            isPlayerDetected = false;
            currentState = EnemyState.Normal;
        }
    }

    public void smallJumpMovement()
    {
        if (enemyRb.velocity.y < 0)
        {
            enemyRb.velocity = new Vector2(enemyRb.velocity.x, 0);
        }

        enemyRb.AddForce(new Vector2(0, jumpForce * 0.5f), ForceMode2D.Impulse);
    }

    bool canAttack()
    {
        return Time.time - lastAttackTime >= attackCooldown;
    }

    void AttackPlayer()
    {
        // Vérifier que le joueur est dans la zone de tir
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRadius && canAttack())
        {
            // Le joueur est dans la zone de tir, déclencher l'événement d'attaque
            if (OnAttack != null)
            {
                OnAttack();
            }
            lastAttackTime = Time.time;
            Debug.Log("L'ennemi attaque le joueur");
        }
    }

    void ReturnToOriginalPosition()
    {
        // Retourner à la position d'origine
        Vector2 direction = (originalPosition - transform.position).normalized;
        enemyRb.velocity = direction * moveSpeed;

        // Vérifier si l'ennemi est retourné à sa position d'origine
        if (Vector2.Distance(transform.position, originalPosition) < 0.1f)
        {
            enemyRb.velocity = Vector2.zero;
            currentState = EnemyState.Normal;
        }
    }
}

