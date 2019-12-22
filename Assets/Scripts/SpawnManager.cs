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
    [SerializeField]
    private Preasent m_preasent = null;

    PoolManager<Enemy> m_pool;
    PoolManager<Preasent> m_preasentPool;

    float? m_timer = null;
    
    #endregion

    #region Properties


    #endregion

    #region Construction

    void Awake()
    {
        m_pool = new PoolManager<Enemy>(m_enemy, 10);
        m_preasentPool = new PoolManager<Preasent>(m_preasent, 10);
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
        float rand = Random.Range(0f, 1f);

        if(rand <= 0.7f)
        {
            Enemy enemy = m_pool.Get();
            enemy.ResetLife();
            Transform spawn = m_locations[UnityEngine.Random.Range(0, m_locations.Length)];
            enemy.transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        }
        else
        {
            Preasent preasent = m_preasentPool.Get();
            Transform spawn = m_locations[UnityEngine.Random.Range(0, m_locations.Length)];
            preasent.transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        }

    }
    
    #endregion

}
