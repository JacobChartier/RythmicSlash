using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [Header("Key codes")]
    [SerializeField] private KeyCode moveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode moveRightKey = KeyCode.D;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Events")]
    [SerializeField][Space] private UnityEvent OnMoveLeftKeyPressed;
    [SerializeField][Space] private UnityEvent OnMoveRightKeyPressed;
    [SerializeField][Space] private UnityEvent OnJumpKeyPressed;

    private void Update()
    {
        if (Input.GetKey(moveLeftKey))
        {
            OnMoveLeftKeyPressed?.Invoke();
        }

        if (Input.GetKey(moveRightKey))
        {
            OnMoveRightKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(jumpKey))
        {
            OnJumpKeyPressed?.Invoke();
        }
    }
}
