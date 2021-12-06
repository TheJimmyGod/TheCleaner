using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum EnemyType { FootGunner, HeavyGunner, SniperGunner}; // spelling has to match the object pool manager

    [SerializeField]
    EnemyType enemyType;

    public Color gizmoColor = Color.green;

    // It's property for waves
    [SerializeField]
    private int enemyCount;

    public int EnemyCount
    {
        get { return enemyCount; }
    }

    public bool StartOnSceneLoad = true;

    private List<GameObject> _activeEnemies = new List<GameObject>();
    
    public void StartSpawner()
    {
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
            _activeEnemies.Add(_enemy);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
