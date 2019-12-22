using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    string m_startScreen = null;


    EnemySpawnManager m_enemySpawnManager = null;

    public Camera activeCamera;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;

        m_enemySpawnManager = FindObjectOfType<EnemySpawnManager>();
        Elf.Died += OnElfDied;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(m_startScreen);
        }
    }

    public void OnElfDied(Vector3 position)
    {
        m_enemySpawnManager.Spawn(position);
    }

}
