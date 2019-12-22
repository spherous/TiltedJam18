using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : Damagable
{
    public override int TakeDamage(int damageToTake)
    {
        return base.TakeDamage(damageToTake);
    } 
    protected override void Death()
    {
        enabled = false;
        base.Death();
    }
    public override void Heal(int amountToHeal)
    {
        base.Heal(amountToHeal);
    }
}
