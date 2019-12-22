using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    GameManager gm => GameManager.Instance;
    Rigidbody2D rb;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Projectile projectile;

    [SerializeField]
    private float attackDelay;
    private float lastAttackTime = 0;
    
    private float powerUpDuration = 0;
    [SerializeField]
    private float maxPowerUpDuration;
    [SerializeField]
    private float powerUpTime;


    [SerializeField]
    private float m_projectileShakeMagnitude = .5f;

    [SerializeField]
    private float m_projectileShakeDuration = .5f;


    PoolManager<Projectile> m_pool;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        m_pool = new PoolManager<Projectile>(projectile, 10);
    }

    private void Update()
    {
        Movement();
        if(Input.GetButton("Fire1"))
            Fire();

        if(powerUpDuration >= 0)
        {
            powerUpDuration -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        float modifiedAttackDelay = attackDelay;

        if(powerUpDuration > 0 )
            modifiedAttackDelay = attackDelay * 0.1f;

        if(Time.time - lastAttackTime >= modifiedAttackDelay)
        {
            lastAttackTime = Time.time;

            Projectile projectile = m_pool.Get();
            projectile.transform.position = transform.position;
            projectile.Fire(gm.activeCamera.ScreenToWorldPoint(Input.mousePosition), 1);

            ScreenShake.Shake(m_projectileShakeMagnitude, m_projectileShakeDuration);
        }
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.AddForce(new float2(x, y) * speed * Time.deltaTime);
    }

    public void PowerUp()
    {
        powerUpDuration = Mathf.Clamp(powerUpDuration + powerUpTime, 0, maxPowerUpDuration);
    }
}
