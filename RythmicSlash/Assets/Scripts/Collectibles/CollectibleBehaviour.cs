using UnityEngine;
using UnityEngine.Events;

public class CollectibleBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent OnCollisionWithPlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Collision detected between: <color=#FFFFFF>{this.gameObject.name}</color> and <color=#FFFFFF>{collision.gameObject.name}</color>");

            CameraEffects.Instance.ShakeCamera(0.1f,0.5f);
            AudioManager.Instance.PlaySound("collectible_pop");
            OnCollisionWithPlayer?.Invoke();
        }
    }
}
