using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected float maxHealth = 5.0f;
    protected float health = 0.0f;

    [SerializeField]
    protected float attackTime = 3.0f;
    [SerializeField]
    protected float movementSpeed = 10.0f;
    [SerializeField]
    protected float attackRange = 10.0f;
    [SerializeField]
    protected float detectPerTime = 0.2f;
    [SerializeField]
    protected float wonderLimit = 10.0f;
    

    protected bool isDeath = false;
    protected bool isDetected = false;
    protected float wonderTimer = 0.0f;


    public Gun enemyGun = null;
    private float timer = 0.0f;
    private float detectionTimer = 0.0f;
   

    public Transform target;

    public NavMeshAgent mAgent;

    void Start()
    {
        health = maxHealth;
        enemyGun = transform.Find("Gun").gameObject.GetComponent<Gun>();
        mAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isDeath)
            return;
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
            return;
        }
        
        if(isDetected)
        {
            timer += Time.deltaTime;
            mAgent.isStopped = true;
            Vector3 toTarget = target.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 4.0f * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                if (timer >= attackTime)
                {
                    Shoot();
                    timer = 0.0f;
                }
            }
        }
        else
        {
            detectionTimer += Time.deltaTime;
            wonderTimer += Time.deltaTime;
            if (detectionTimer >= detectPerTime)
            {
                isDetected = GetComponent<VisualSensor>().FindingTarget();
                detectionTimer = 0.0f;
            }
            var angle = GetComponent<VisualSensor>().angle + transform.rotation.y;
            if (wonderTimer >= wonderLimit / 2.0f)
                transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * -angle);
            else
                transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * angle);
        }

        Move();
    }

    public void TakeDamage(float value)
    {
        if(!isDeath)
        {
            health -= value;
            isDetected = true;
            if (health <= 0.0f)
                StartCoroutine(Death());
        }
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
        {
            if (Vector3.Distance(transform.position, target.position) > attackRange)
                transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        }
    }

    protected virtual IEnumerator Death()
    {
        isDeath = true;
        yield return new WaitForSeconds(3.0f);
        ServiceLocator.Get<ObjectPoolManager>().RecycleObject(this.gameObject);
    }
}
