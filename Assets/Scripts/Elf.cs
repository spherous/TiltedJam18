using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Elf : Damagable, IPooledObject<Elf>
{
    #region Constants


    #endregion

    #region Variables

    [SerializeField]
    float m_speed = 1f;

    [SerializeField]
    float m_clearanceRange = 20f;

    Rigidbody2D m_rigidbody2D = null;

    float m_originalStartingY = 0f;
    HealthBar bar = null;

    public static float conversion_time = 2f;

    #endregion

    #region Callbacks

    public static Action Survived { get; set; }

    public static Action<Vector3> Died { get; set; }

    public Action<Elf> ReturnToPool { get; set; }

    #endregion

    #region Properties


    #endregion

    #region Construction

    protected override void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();

        base.Awake();
    }

    private void OnEnable()
    {
        m_originalStartingY = Mathf.Abs(this.transform.position.y);
    }

    #endregion

    #region Update

    void FixedUpdate()
    {
        m_rigidbody2D.AddForce(Vector3.down * (m_speed * Time.fixedDeltaTime), ForceMode2D.Force);
        
        if((Mathf.Abs(this.transform.position.y) - m_originalStartingY) > m_clearanceRange)
        {
            Survived?.Invoke();
            ReturnToPool(this);
        }
    }

    #endregion

    #region Physics

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy)
        {
            Death();
        }
    }

    #endregion

    #region Management

    protected override void Death()
    {
        // perform death
        //ReturnToPool(this);
        enabled = false;
        Collider2D cd = GetComponent<Collider2D>();
        if (cd) cd.enabled = false;

        Died?.Invoke(this.transform.position);

        WalkingAnimator wa = GetComponentInChildren<WalkingAnimator>();
        if (wa) wa.DeathCheck(0);

        bar = GetComponentInChildren<HealthBar>();
        if (bar) bar.gameObject.SetActive(false);
        Invoke("Repool", conversion_time);
    }

    private void Repool()
    {
        enabled = true;
        Collider2D cd = GetComponent<Collider2D>();
        if (cd) cd.enabled = true;
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

}
