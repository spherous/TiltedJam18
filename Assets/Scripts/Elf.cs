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

    #endregion

    #region Callbacks


    public static Action<Vector3> Died { get; set; }

    public Action<Elf> ReturnToPool { get; set; }

    #endregion

    #region Properties


    #endregion

    #region Construction

    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Update

    void FixedUpdate()
    {
        //if (ms_santa)
        //{
        //    m_rigidbody2D.AddForce((ms_santa.transform.position - transform.position) * (m_speed * Time.fixedDeltaTime), ForceMode2D.Force);
        //}
    }

    #endregion

    #region Physics



    #endregion

    #region Management

    protected override void Death()
    {
        // perform death
        ReturnToPool(this);

        Died?.Invoke(this.transform.position);
    }

    public override void ResetLife()
    {
        base.ResetLife();
    }
    
    #endregion

}
