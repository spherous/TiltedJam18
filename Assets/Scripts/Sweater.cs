using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweater : MonoBehaviour, IPooledObject<Sweater>
{
    public Action<Sweater> ReturnToPool {get; set;}
    [SerializeField]
    private int amountToHeal;
    private void OnTriggerEnter2D(Collider2D other) {
        Damagable damagable = other.GetComponent<Damagable>();
        if(damagable != null)
        {
            if(damagable is Santa)
            {
                damagable.Heal(amountToHeal);
                ReturnToPool(this);
            }
            if(damagable is Elf)
                ReturnToPool(this);
        }

    }
}
