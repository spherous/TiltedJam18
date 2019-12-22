using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : Damagable
{
    public static Action ProperlyDead;

    public override int TakeDamage(int damageToTake)
    {
        return base.TakeDamage(damageToTake);
    } 
    protected override void Death()
    {
        enabled = false;
        base.Death();

        ProperlyDead?.Invoke();
    }
    public override void Heal(int amountToHeal)
    {
        base.Heal(amountToHeal);
    }
}
