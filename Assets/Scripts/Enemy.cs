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
        // perform death
        ReturnToPool(this);
    }

    #endregion

}
