using System;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IPooledObject<Projectile>
{
    public Rigidbody2D rb;
    public float force;

    public int damage;

    [SerializeField]
    private ParticleSystem collisionEffect;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private TrailRenderer trail;
    [SerializeField]
    private float decayTime;
    public Action<Projectile> ReturnToPool { get; set; }

    private void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector3 fireAtLocation, int damage)
    {
        this.damage = damage;
        fireAtLocation.z = 0;
        Debug.DrawRay(transform.position, fireAtLocation - transform.position, Color.red, 1f);
        transform.up = fireAtLocation - transform.position;
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damagable hit = collision.gameObject.GetComponent<Damagable>();
        if(hit != null)
            hit.TakeDamage(damage);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        sprite.enabled = false;
        collisionEffect.Play();
        StartCoroutine(Decaying());        
    }

    IEnumerator Decaying()
    {
        yield return new WaitForSeconds(decayTime);
        trail.Clear();
        sprite.enabled = true;
        ReturnToPool(this);
    }
}
