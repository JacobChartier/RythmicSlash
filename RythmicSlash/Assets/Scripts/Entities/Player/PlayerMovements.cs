using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private int mouvementSpeed = 3;
    [SerializeField] private int maxJumps = 2, currentJumps = 0;
    [SerializeField] private float jumpForce = 11.5f;

    [SerializeField] private bool isPlayerOnGround = false;

    [Header("Player movement")]
    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 positionGoal;
    [SerializeField] public bool positionGoalReach = false;
    [SerializeField] public int currentDirection = 0;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();

        positionGoal = new Vector3(player.position.x, player.position.y - 0.5f);
        velocity = Vector3.zero;
    }

    private void Update()
    {
        isPlayerOnGround = IsPlayerOnGround();

        if (isPlayerOnGround)
            currentJumps = 0;

        if (player.transform.position == positionGoal)
            positionGoalReach = true;
        else if (!positionGoalReach)
            player.transform.position = Vector3.SmoothDamp(player.transform.position, positionGoal, ref velocity, 0.1f);


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
            smallJumpMouvement();
        }

        FlipSprite(direction);

    }

    public void smallJumpMouvement()
    {
        if (player.velocity.y < 0)
        {
           player.velocity = new Vector2(player.velocity.x, 0);
        }
        player.AddForce(new Vector2(0, jumpForce * 0.5f), ForceMode2D.Impulse);
    }

    public void Jump()
    {
        if (isPlayerOnGround || currentJumps < maxJumps)
        {
            player.velocity = new Vector2(player.velocity.x, 0);
            player.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            currentJumps++;
        }
    }

    private void FlipSprite(int direction)
    {
        if (direction == 1)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction == -1)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private bool IsPlayerOnGround()
    {
        if (player.velocity.y < -0.05 || player.velocity.y > 0.05)
            return true;
        else
            return false;
    }
}
