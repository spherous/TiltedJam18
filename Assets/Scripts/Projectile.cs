using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IPooledObject<Projectile>
{
    public Rigidbody2D rb;
    public float force;

    public Action<Projectile> ReturnToPool { get; set; }

    private void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector3 fireAtLocation)
    {
        fireAtLocation.z = 0;
        Debug.DrawRay(transform.position, fireAtLocation - transform.position, Color.red, 1f);
        transform.LookAt(fireAtLocation);
        rb.AddForce(transform.forward * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReturnToPool(this);
    }

}
