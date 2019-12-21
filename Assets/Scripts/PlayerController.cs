﻿using System;
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
    }

    private void Fire()
    {
        if(Time.time - lastAttackTime >= attackDelay)
        {
            lastAttackTime = Time.time;

            Projectile projectile = m_pool.Get();
            projectile.transform.position = transform.position;
            projectile.Fire(gm.activeCamera.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.AddForce(new float2(x, y) * speed * Time.deltaTime);
    }
}
