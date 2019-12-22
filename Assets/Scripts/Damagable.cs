using UnityEngine;

public class Damagable : MonoBehaviour
{

    public delegate void HealthChanged(int newHealth);
    public HealthChanged OnHealthChanged;
    [SerializeField]
    public int maxHealth;
    public int currentHealth { get; private set; } = 0;

    private void Awake() {
        ResetLife();
    }

    public virtual int TakeDamage(int damageToTake)
    {
        currentHealth = Mathf.Clamp(currentHealth - damageToTake, 0, maxHealth);

        if(currentHealth == 0)
            Death();


        OnHealthChanged?.Invoke(currentHealth);
        
        return currentHealth;
    }

    protected virtual void Death()
    {
        Debug.Log(name + " has died.");
    }

    public virtual void ResetLife()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    public virtual void Heal(int amountToHeal)
    {
        currentHealth = Mathf.Min(currentHealth + Mathf.Abs(amountToHeal), maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
    }
}
