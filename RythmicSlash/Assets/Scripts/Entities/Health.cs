using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int MAX_HEALTH = 10;
    [SerializeField] public int currentHealth { get; private set; }

    [SerializeField] private UnityEvent OnGainHealth;
    [SerializeField] private UnityEvent OnLoseHealth;
    [SerializeField] private UnityEvent OnDeath;

    void Awake()
    {
        currentHealth = MAX_HEALTH;
    }

    public void GainHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, MAX_HEALTH);

        OnGainHealth?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, MAX_HEALTH);

        OnLoseHealth?.Invoke();

        if (currentHealth < 1)
            OnDeath?.Invoke();
    }
}
