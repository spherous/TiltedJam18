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

    int m_score = 0;
    public TextMeshProUGUI scoreText;

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

    public void AddToScore(int value)
    {
        m_score += value;
        UpdateScore();
    }

    public void OnElfDied(Vector3 position)
    {
        score--;
        StartCoroutine(ConvertElf(position));
    }

    IEnumerator ConvertElf(Vector3 pos)
    {
        yield return new WaitForSeconds(Elf.conversion_time);
        Enemy enemy = m_enemySpawnManager.Spawn(pos);
        enemy.ResetLife();
    }

    void UpdateScore()
    {
        scoreText.text = m_score.ToString();
        SavedScore.SaveScore(m_score);
    }

}
