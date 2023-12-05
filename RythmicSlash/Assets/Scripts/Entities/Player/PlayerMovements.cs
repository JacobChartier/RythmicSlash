using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private int movementSpeed = 3;
    [SerializeField] private int maxJumps = 2, currentJumps = 0;
    [SerializeField] private float jumpForce = 2;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private bool LookingLeft;

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
            player.transform.position = Vector3.Lerp(player.transform.position, positionGoal, Time.deltaTime);
            //player.transform.position = Vector3.SmoothDamp(player.transform.position, positionGoal, ref velocity, 0.1f);

        // Gestion du cooldown d'attaque
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack >= attackCooldown)
        {
            canAttack = true;
        }
    }

    public void Move(int direction)
    {
        float moveAmount = direction * movementSpeed * Time.deltaTime;

        positionGoalReach = false;
        currentDirection = direction;
        positionGoal = new Vector3(Mathf.Round(player.position.x + direction), player.position.y);

        player.transform.position = Vector3.Lerp(player.transform.position, positionGoal, Time.deltaTime);
        //player.transform.position = Vector3.SmoothDamp(player.transform.position, positionGoal, ref velocity, 0.1f);

        player.velocity = new Vector2(moveAmount, player.velocity.y);

        if (!isPlayerOnGround)
        {
            SmallJump(direction);
            movementSpeed = 3;
        }

        FlipSprite(direction);
    }

    private void SmallJump(int direction)
    {
        if (!isPlayerOnGround || currentJumps < maxJumps)
        {
            player.velocity = new Vector2(player.velocity.x, 0);
            player.AddForce(new Vector2((direction * 0.6f), jumpForce), ForceMode2D.Impulse);
            currentJumps++;
        }
    }

    public void Jump(float intensity)
    {
        if (!isPlayerOnGround || currentJumps < maxJumps)
        {
            if (LookingLeft)
            {
                player.velocity = new Vector2(player.velocity.x, 0);
                player.AddForce(new Vector2(-movementSpeed * 0.85f, jumpForce * intensity), ForceMode2D.Impulse);

                positionGoal = new Vector3(Mathf.Round(player.position.x + -3f), player.position.y);
                player.transform.position = Vector3.Lerp(player.transform.position, positionGoal, Time.deltaTime);
                //player.transform.position = Vector3.SmoothDamp(player.transform.position, positionGoal, ref velocity, 0.1f);

                currentJumps++;
                movementSpeed = 3;
            }
            else
            {
                player.velocity = new Vector2(player.velocity.x, 0);
                player.AddForce(new Vector2(movementSpeed * 0.85f, jumpForce * intensity), ForceMode2D.Impulse);

                positionGoal = new Vector3(Mathf.Round(player.position.x + 3f), player.position.y);
                player.transform.position = Vector3.Lerp(player.transform.position, positionGoal, Time.deltaTime);
                //player.transform.position = Vector3.SmoothDamp(player.transform.position, positionGoal, ref velocity, 0.1f);

                currentJumps++;
                movementSpeed = 3;
            }
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
        {
            LookingLeft = false;
            player.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction == -1)
        {
            LookingLeft = true;
            player.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private bool IsPlayerOnGround()
    {
        if (player.velocity.y < -0.02 || player.velocity.y > 0.02)
            return true;
        else
            return false;
    }
}
