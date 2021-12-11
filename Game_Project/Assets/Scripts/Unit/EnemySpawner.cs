using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum EnemyType { FootGunner, HeavyGunner, SniperGunner}; // spelling has to match the object pool manager

    [SerializeField]
    EnemyType enemyType;
    [SerializeField]
    private float angle = 0.0f;
    public Color gizmoColor = Color.green;
    private GameObject spawnManagerGO;
    // It's property for waves
    [SerializeField]
    private int enemyCount;

    public int EnemyCount
    {
        get { return enemyCount; }
    }
    public bool StartOnSceneLoad = true;
    
    public void StartSpawner()
    {
        spawnManagerGO = GameObject.FindGameObjectWithTag("SpawnManager").gameObject;
        StartCoroutine("BeginWaveSpawn");
    }

    private IEnumerator BeginWaveSpawn()
    {
        Spawn();
        yield return null;
    }

    public void Spawn()
    {
        for (int i = 0; i < enemyCount; ++i)
        {
            GameObject _enemy = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(enemyType.ToString());
            _enemy.transform.position = transform.position;
            _enemy.SetActive(true);
            _enemy.GetComponent<Enemy>().Restore();
            _enemy.GetComponent<Enemy>().enemyGun.Restore();
            _enemy.transform.Rotate(0.0f, angle, 0.0f);
            spawnManagerGO.GetComponent<EnemySpawnManager>().EnemyList.Add(_enemy);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
