using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    public int currentHealth { get; private set; } = 0;

    public virtual int TakeDamage(int damageToTake)
    {
        currentHealth = Mathf.Clamp(currentHealth - damageToTake, 0, maxHealth);

        if(currentHealth == 0)
            Death();

        return currentHealth;
    }

    protected virtual void Death()
    {
        Debug.Log(name + " has died.");
    }

    public virtual void ResetLife()
    {
        currentHealth = maxHealth;
    }

    public virtual void Heal(int amountToHeal)
    {
        currentHealth = Mathf.Min(currentHealth + amountToHeal, maxHealth);
    }
}
