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
    private SpriteRenderer[] sprite;
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

    [SerializeField]
    private float radius;

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
        Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius);
        foreach(Collider2D c in cols)
        {
            Damagable hit = c.gameObject.GetComponent<Damagable>();
            if(hit != null)
            {
                if(hit is Elf)
                    continue;
                if(hit.currentHealth > 0)
                    hit.TakeDamage(damage);
            }
        }
        // Damagable hit = collision.gameObject.GetComponent<Damagable>();

        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        foreach(SpriteRenderer sr in sprite)
            sr.enabled = false;
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
        foreach(SpriteRenderer sr in sprite)
            sr.enabled = true;
        collider.enabled = true;
        ReturnToPool(this);
    }
}
