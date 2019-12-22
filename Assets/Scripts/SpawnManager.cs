using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnManager<T>  : MonoBehaviour where T : MonoBehaviour, IPooledObject<T>
{
    #region Constants


    #endregion

    #region Variables

    [SerializeField]
    int m_initialAmount = 10;

    [SerializeField]
    float m_spawnTime = 2f;

    [SerializeField]
    Transform[] m_locations = null;

    [SerializeField]
    private T m_pooledItem = null;
    
    PoolManager<T> m_pool;


    float? m_timer = null;
    
    #endregion

    #region Properties


    #endregion

    #region Construction

    void Awake()
    {
        m_pool = new PoolManager<T>(m_pooledItem, m_initialAmount);
    }

    #endregion

    #region Update

    void Update()
    {
        if (!m_timer.HasValue)
            m_timer = m_spawnTime;

        m_timer -= Time.deltaTime;

        if (m_timer < 0f)
        {
            Spawn();
            m_timer = m_spawnTime;
        }
    }

    #endregion

    #region Management

    public virtual T Spawn()
    {
        T pooledItem = m_pool.Get();
        Transform spawn = m_locations[UnityEngine.Random.Range(0, m_locations.Length)];
        pooledItem.transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        return pooledItem;
    }

    public virtual T Spawn(Vector3 position)
    {
        T pooledItem = m_pool.Get();
        pooledItem.transform.SetPositionAndRotation(position, Quaternion.identity);
        return pooledItem;
    }

    #endregion

}
