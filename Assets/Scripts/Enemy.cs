using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Damagable, IPooledObject<Enemy>
{
    #region Constants


    #endregion

    #region Variables

    [SerializeField]
    float m_speed = 1f;
    
    static Santa ms_santa = null;

    Rigidbody2D m_rigidbody2D = null;

    [SerializeField]
    private int damageToDeal;

    private HealthBar bar = default;
    #endregion

    #region Callbacks

    public Action<Enemy> ReturnToPool { get; set; }

    #endregion

    #region Properties


    #endregion

    #region Construction

    void Awake()
    {
        if (ms_santa == null)
            ms_santa = FindObjectOfType<Santa>();

        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Update

    void FixedUpdate()
    {
        if(ms_santa)
        {
            m_rigidbody2D.AddForce((ms_santa.transform.position - transform.position) * (m_speed * Time.fixedDeltaTime), ForceMode2D.Force);
        }
    }

    #endregion

    #region Management
    
    protected override void Death()
    {
        base.Death();
        // perform death
        //ReturnToPool(this);
        //Collider2D cd = GetComponent<Collider2D>();
        //if (cd) cd.enabled = false;
        enabled = false;

        bar = GetComponentInChildren<HealthBar>();
        if (bar) bar.gameObject.SetActive(false);
        Invoke("Repool", 10f);
        GameManager.Instance.AddToScore(2);
    }

    private void Repool()
    {
        Collider2D cd = GetComponent<Collider2D>();
        if (cd) cd.enabled = true;
        enabled = true;
        if (bar) bar.gameObject.SetActive(true);
        WalkingAnimator wa = GetComponentInChildren<WalkingAnimator>();
        if (wa) wa.enabled = true;
        ReturnToPool(this);
    }

    public override void ResetLife()
    {
        base.ResetLife();
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D other) {
        Santa santa = other.gameObject.GetComponent<Santa>();
        if(santa != null)
            santa.TakeDamage(damageToDeal);
    }
}
