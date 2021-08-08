using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance = null;
    public List<Vector2> enemySpawns;
    public GameObject enemyPrefab;
    public List<string> openGates;

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
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnDeadEnemies()
    {
        foreach (Vector2 pos in enemySpawns)
        {
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
        enemySpawns.Clear();
        OpenGates();
    }

    void OpenGates()
    {
        foreach (string gate in openGates)
        {
            GameObject.Find(gate).GetComponent<Gate>().OpenAgain();
        }
    }

}
