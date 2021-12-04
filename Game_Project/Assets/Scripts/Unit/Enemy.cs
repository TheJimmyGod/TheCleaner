using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected float maxHealth = 10.0f;
    protected float health = 10.0f;
    [SerializeField]
    protected float attackTime = 3.0f;
    [SerializeField]
    protected float movementSpeed = 10.0f;
    [SerializeField]
    protected float detectRange = 10.0f;

    [SerializeField]
    protected bool isDeath = false;

    private bool isDetected = false;

    public Gun enemyGun = null;
    private float timer = 0.0f;

    public Transform target;

    void Start()
    {
        enemyGun = GetComponent<Gun>();
    }

    void Update()
    {
        if (isDeath) return;
        timer += Time.deltaTime;
        if(timer >= attackTime)
        {
            Shoot();
            timer = 0.0f;
        }

        // TODO : Create Detection
    }

    public void TakeDamage(float value)
    {
        health -= value;
        if(health <= 0.0f)
            Death();
    }

    public void Shoot()
    {
        if(enemyGun && target) enemyGun.Shoot();
    }

    public void Restore()
    {
        health = maxHealth;
        isDeath = false;
    }

    protected virtual void Move()
    {
        if(isDetected)
            if(Vector3.Distance(transform.position, target.position) > detectRange)
                transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
    }

    protected virtual IEnumerator Death()
    {
        isDeath = true;
        // TODO: Play animation
        yield return new WaitForSeconds(3.0f);
        ServiceLocator.Get<ObjectPoolManager>().RecycleObject(this.gameObject);
    }
}
