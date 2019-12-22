using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preasent : MonoBehaviour, IPooledObject<Preasent>
{
    public Action<Preasent> ReturnToPool { get; set; }
    private void OnTriggerEnter2D(Collider2D other) { 
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if(pc != null)
        {
            pc.PowerUp();
            ReturnToPool(this);
        }
        Damagable damagable = other.gameObject.GetComponent<Damagable>();
        if(damagable != null)
            if(damagable is Elf)
                ReturnToPool(this);
    }
}
