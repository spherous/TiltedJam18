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
    private Collider2D collider;
    [SerializeField]
    private float decayTime;
    public Action<Projectile> ReturnToPool { get; set; }

    [SerializeField]
    AudioClip[] m_hitSounds = null;

    AudioSource m_audioSource = null;

    private void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();

        m_audioSource = GetComponent<AudioSource>();
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
        collider.enabled = false;
        StartCoroutine(Decaying());

        m_audioSource.clip = m_hitSounds[UnityEngine.Random.Range(0, m_hitSounds.Length)];
        m_audioSource.Play();
    }

    IEnumerator Decaying()
    {
        yield return new WaitForSeconds(decayTime);
        trail.Clear();
        sprite.enabled = true;
        collider.enabled = true;
        ReturnToPool(this);
    }
}
