using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenShake : MonoBehaviour
{
    #region Constants


    #endregion

    #region Variables

    static ScreenShake ms_instance = null;

    // state
    Vector3 m_initialPosition;
    float? m_timer = null;
    float m_occuranceTimer = 0;
    public float m_occuranceTime;
    float m_shakeMagnitude = 1f;
    
    #endregion

    #region Properties


    #endregion

    #region Construction

    void Start()
    {
        ms_instance = this;
    }

    #endregion

    #region Update

    void Update()
    {
        if (m_timer.HasValue)
        {
            m_timer -= Time.deltaTime;
            m_occuranceTimer += Time.deltaTime;
            if(m_occuranceTimer >= m_occuranceTime)
            {
                m_occuranceTimer = 0;
                transform.localPosition = Vector3.Lerp(transform.localPosition, m_initialPosition + Random.insideUnitSphere * m_shakeMagnitude, Time.fixedDeltaTime);
            }

            if (m_timer < 0f)
            {
                transform.localPosition = m_initialPosition;
                m_timer = null;
            }
        }
    }

    #endregion

    #region Management

    public static void Shake(float magnitude, float duration )
    {
        if (ms_instance)
        {
            ms_instance.m_initialPosition = ms_instance.transform.localPosition;

            ms_instance.m_shakeMagnitude = magnitude;
            ms_instance.m_timer = duration;
        }
    }

    #endregion

}
