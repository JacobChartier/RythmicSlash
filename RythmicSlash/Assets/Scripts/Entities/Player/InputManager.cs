using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [Header("Key codes")]
    [SerializeField] private KeyCode moveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode moveRightKey = KeyCode.D;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode shootKey = KeyCode.F;
    [SerializeField] private KeyCode pauseGameKey = KeyCode.Escape;

    [Header("Events")]
    [SerializeField][Space] private UnityEvent OnMoveLeftKeyPressed;
    [SerializeField][Space] private UnityEvent OnMoveRightKeyPressed;
    [SerializeField][Space] private UnityEvent OnJumpKeyPressed;
    [SerializeField][Space] private UnityEvent OnShootKeyPressed;
    [SerializeField][Space] private UnityEvent OnPauseGameKeyPressed;

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
            Debug.Log($"A key has been pressed: <color=#FFFF00>{jumpKey}</color>");
        }

        if (Input.GetKeyDown(shootKey))
        {
            OnShootKeyPressed?.Invoke();
            Debug.Log($"A key has been pressed: <color=#FFFF00>{shootKey}</color>");
        }

        if (Input.GetKeyDown(pauseGameKey))
        {
            OnPauseGameKeyPressed?.Invoke();
            Debug.Log($"A key has been pressed: <color=#FFFF00>{pauseGameKey}</color>");
        }
    }
}
