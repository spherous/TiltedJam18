using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPooledObject<T>  where T : MonoBehaviour
{
    Action<T> ReturnToPool { get; set; }
}


public class PoolManager<T> where T : MonoBehaviour, IPooledObject<T>
{
    #region Constants


    #endregion

    #region Variables

    Queue<T> m_pool = new Queue<T>();

    T m_prefab = null;
    Transform m_parent = null;

    #endregion

    #region Properties


    #endregion

    #region Construction

    public PoolManager(T prefab, int startingCount = 0, Transform parent = null)
    {
        m_prefab = prefab;
        m_parent = parent;

        for (int i=0;i<startingCount;i++)
        {
            T obj = Create();
            m_pool.Enqueue(obj);
        }
    }

    #endregion
    
    #region Management

    void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        m_pool.Enqueue(obj);
    }

    T Create(bool active = false)
    {
        T obj = GameObject.Instantiate<T>(m_prefab, m_parent);
        obj.ReturnToPool += Return;
        obj.gameObject.SetActive(active);
        return obj;
    }
    
    public T Get()
    {
        if(m_pool.Count > 0)
        {
            T obj = m_pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            return Create(true);
        }

    }

    #endregion

}
