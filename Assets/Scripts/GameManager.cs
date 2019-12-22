using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public int score = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        Instance = this;

        m_enemySpawnManager = FindObjectOfType<EnemySpawnManager>();
        Elf.Died += OnElfDied;
    }
    
    void Update()
    {
        scoreText.text = score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(m_startScreen);
        }
    }

    public void OnElfDied(Vector3 position)
    {
        score--;
        Enemy enemy = m_enemySpawnManager.Spawn(position);
        enemy.ResetLife();
    }

}
