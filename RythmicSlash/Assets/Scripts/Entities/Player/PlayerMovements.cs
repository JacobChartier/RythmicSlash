using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private int mouvementSpeed = 3;
    [SerializeField] private int maxJumps = 2, currentJumps = 0;
    [SerializeField] private float jumpForce = 12.5f;
    [SerializeField] private float attackCooldown = 5f;

    [SerializeField] private bool isPlayerOnGround = true;
    private bool canAttack = true; // Permet d'attaquer uniquement si cette variable est vraie
    private float timeSinceLastAttack = 0f;

    [Header("Player movement")]
    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 positionGoal;
    [SerializeField] public bool positionGoalReach = false;
    [SerializeField] public int currentDirection = 0;

    private FiringBehaviour firingBehaviour;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();

        velocity = Vector3.zero;
        positionGoal = new Vector3(player.position.x, player.position.y - 0.5f);

        firingBehaviour = FindObjectOfType<FiringBehaviour>();
    }

    private void Update()
    {
        isPlayerOnGround = IsPlayerOnGround();

        if (!isPlayerOnGround)
            currentJumps = 0;

        if (player.transform.position == positionGoal)
            positionGoalReach = true;
        else if (!positionGoalReach)
            player.transform.position = Vector3.SmoothDamp(player.transform.position, positionGoal, ref velocity, 0.1f);

        // Gestion du cooldown d'attaque
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack >= attackCooldown)
        {
            canAttack = true;
        }
    }

    public void Move(int direction)
    {
        float moveAmount = direction * mouvementSpeed * Time.deltaTime;

        positionGoalReach = false;
        currentDirection = direction;
        positionGoal = new Vector3(Mathf.Round(player.position.x + direction), player.position.y);

        player.transform.position = Vector3.SmoothDamp(player.transform.position, positionGoal, ref velocity, 0.1f);

        player.velocity = new Vector2(moveAmount, player.velocity.y);

        if (!isPlayerOnGround)
        {
            Jump(0.5f);
        }

        FlipSprite(direction);
    }

    public void Jump(float intensity = 1.0f)
    {
        if (!isPlayerOnGround || currentJumps < maxJumps)
        {
            player.velocity = new Vector2(player.velocity.x, 0);
            player.AddForce(new Vector2(0, jumpForce * intensity), ForceMode2D.Impulse);
            currentJumps++;
        }
    }

    public void Attack()
    {
        if(canAttack)
        {
            firingBehaviour.FireProjectile();

            canAttack = false;
            timeSinceLastAttack = 0;
        }
    }

    private void FlipSprite(int direction)
    {
        if (direction == 1)
            player.transform.localScale = new Vector3(1, 1, 1);
        else if (direction == -1)
            player.transform.localScale = new Vector3(-1, 1, 1);
    }

    private bool IsPlayerOnGround()
    {
        if (player.velocity.y < -0.02 || player.velocity.y > 0.02)
            return true;
        else
            return false;
    }
}
