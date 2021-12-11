using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public List<EnemySpawner> Spawners { get; set; }
    public List<GameObject> EnemyList;

    private void Awake()
    {
        Spawners = new List<EnemySpawner>(FindObjectsOfType<EnemySpawner>());
        EnemyList = new List<GameObject>();
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

    public void Register(Transform transform)
    {
        EnemyList.Add(transform.gameObject);
    }

    public void UnRegister(Transform transform)
    {
        EnemyList.Remove(transform.gameObject);
    }
}
