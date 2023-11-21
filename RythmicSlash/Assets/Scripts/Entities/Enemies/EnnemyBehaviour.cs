using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform player;
    public float detectionRadius = 5f;
    public float attackRadius = 2f;
    private float moveSpeed = 4f;
    public float attackCooldown = 3f;
    public float jumpForce = 0.2f;

    private Rigidbody2D enemyRb;
    private Vector3 originalPosition;
    private bool isReturning = false;
    private bool isAttacking = false;
    private bool isEnnemyOnGround = false;
    private bool isPlayerDetected = false;

    public delegate void AttackEventHandler();
    public event AttackEventHandler OnAttack;
    private float lastAttackTime;
    private EnemyState currentState = EnemyState.Normal;

    private enum EnemyStateà
    {
        Normal,
        MoveToPlayer,
        Attack,
        ReturnToOriginal,
    }

    private void Start()
    {
        originalPosition = transform.position;
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Normal:
                NormalBehavior();
                break;
            case EnemyState.MoveToPlayer:
                MoveToPlayer();
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
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Le joueur est détecté
            isPlayerDetected = true;
            currentState = EnemyState.MoveToPlayer;
        }
        else if (isPlayerDetected)
        {
            // Le joueur s'est déplacé hors de la zone de détection
            currentState = EnemyState.ReturnToOriginal;
        }

        // Autres comportements normaux ici
    }

    void MoveToPlayer()
    {
        // Orientation directe vers le joueur
        transform.LookAt(player);

        // Déplacement vers le joueur
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);

        // Vérifier si l'ennemi est assez proche pour attaquer
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRadius)
        {
            currentState = EnemyState.Attack;
        }
    }
    void FlipSprite(int direction)
    {
        if (direction > 0)
        {
            // Laissez la sprite comme elle est (face vers la droite)
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction < 0)
        {
            // Inverser la sprite (face vers la gauche)
            transform.localScale = new Vector3(-1, 1, 1);
        }
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

    // Assurez-vous d'appeler cette fonction lorsque l'ennemi est au sol
    void SetIsEnemyOnGround(bool isOnGround)
    {
        isEnnemyOnGround = isOnGround;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur est détecté, passe en mode attaque
            isAttacking = true;
        }
    }

    void returningToOriginalPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

        if (transform.position == originalPosition)
        {
            isReturning = false;
        }
    }
}

