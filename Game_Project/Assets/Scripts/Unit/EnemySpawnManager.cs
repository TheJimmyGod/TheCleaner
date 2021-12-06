using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public List<EnemySpawner> Spawners { get; set; }

    private void Awake()
    {
        Spawners = new List<EnemySpawner>(FindObjectsOfType<EnemySpawner>());
    }

    void Start()
    {
        foreach (var spawner in Spawners)
        {
            if (spawner.StartOnSceneLoad == true)
            {
                spawner.StartSpawner();
            }
        }
    }

    public void StartSpawer(EnemySpawner spawner)
    {
        spawner.StartSpawner();
    }

    public void StartAllSpawners()
    {
        foreach (var spawner in Spawners)
        {
            spawner.StartSpawner();
        }
    }
}
