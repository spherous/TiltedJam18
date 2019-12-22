using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : Damagable
{
    public static Action ProperlyDead;

    [SerializeField]
    AudioClip[] m_pickupSounds = null;

    AudioSource m_audioSource = null;

    protected override void Awake()
    {
        base.Awake();
        m_audioSource = GetComponent<AudioSource>();
    }

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

        m_audioSource.clip = m_pickupSounds[UnityEngine.Random.Range(0, m_pickupSounds.Length)];
        m_audioSource.Play();
    }
}
