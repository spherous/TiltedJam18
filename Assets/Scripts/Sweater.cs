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
        Santa santa = other.GetComponent<Santa>();
        if(santa != null)
        {
            santa.Heal(amountToHeal);
            ReturnToPool(this);
        }
    }
}
