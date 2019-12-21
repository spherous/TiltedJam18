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
    private GameObject projectile;

    [SerializeField]
    private float attackDelay;
    private float lastAttackTime = 0;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
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

            GameObject newGO = Instantiate(projectile);
            newGO.transform.position = transform.position;
            newGO.GetComponent<Projectile>().Fire(gm.activeCamera.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.AddForce(new float2(x, y) * speed * Time.deltaTime);
    }
}
