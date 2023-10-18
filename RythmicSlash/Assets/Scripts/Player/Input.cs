using UnityEngine;
using UnityEngine.Events;

public class Input : MonoBehaviour
{
    [SerializeField] private KeyCode MoveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode MoveRightKey = KeyCode.D;

    [SerializeField] private UnityEvent OnMoveLeftKeyPressed;
    [SerializeField] private UnityEvent OnMoveRightKeyPressed;

    private void Update()
    {
        if (UnityEngine.Input.GetKey(MoveLeftKey))
        {
            OnMoveLeftKeyPressed?.Invoke();
        }

        if (UnityEngine.Input.GetKey(MoveRightKey))
        {
            OnMoveRightKeyPressed?.Invoke();
        }
    }
}
