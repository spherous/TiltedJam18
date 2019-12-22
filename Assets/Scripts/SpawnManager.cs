using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    #region Constants


    #endregion

    #region Variables

    [SerializeField]
    float m_spawnTime = 2f;

    [SerializeField]
    Transform[] m_locations = null;

    [SerializeField]
    private Enemy m_enemy = null;

    PoolManager<Enemy> m_pool;

    float? m_timer = null;
    
    #endregion

    #region Properties


    #endregion

    #region Construction

    void Awake()
    {
        m_pool = new PoolManager<Enemy>(m_enemy, 10);
    }

    #endregion

    #region Update

    void Update()
    {
        if (!m_timer.HasValue)
            m_timer = m_spawnTime;

        m_timer -= Time.deltaTime;

        if(m_timer < 0f)
        {
            Spawn();
            m_timer = m_spawnTime;
        }
    }

    #endregion

    #region Management

    void Spawn()
    {
        Enemy enemy = m_pool.Get();
        Transform spawn = m_locations[UnityEngine.Random.Range(0, m_locations.Length)];
        enemy.transform.SetPositionAndRotation(spawn.position, spawn.rotation);
    }
    
    #endregion

}
