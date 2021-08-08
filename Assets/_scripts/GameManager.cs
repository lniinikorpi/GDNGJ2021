using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isAlive = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        EnemySpawner.instance.SpawnDeadEnemies();
    }

    public void EndLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void PlayerDie()
    {
        isAlive = false;
        UIManagerGame.instance.diePanel.SetActive(true);
    }

    public void RestartGame()
    {
        EnemySpawner.instance.enemySpawns.Clear();
        EnemySpawner.instance.openGates.Clear();
        EndLevel();
    }

    public void GameComplete()
    {
        UIManagerGame.instance.completeGamePanel.SetActive(true);
        isAlive = false;
    }

}
