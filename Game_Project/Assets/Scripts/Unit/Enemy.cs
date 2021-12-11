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

    public bool isDead
    {
        get { return isDeath; }
    }

    public Gun enemyGun = null;
    private float timer = 0.0f;
    private float detectionTimer = 0.0f;

    public Animator animator;
    public GameObject dropItem;
    private Rigidbody rb;
    public Transform target;
    public NavMeshAgent mAgent;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        health = maxHealth;
        mAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update() => OnUpdate();

    protected virtual void OnUpdate()
    {
        if (isDeath) return;
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
            return;
        }

        if (isDetected)
        {
            timer += Time.deltaTime;
            mAgent.isStopped = true;
            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                animator.SetBool("Shoot", true);
                if (animator.IsInTransition(0))
                    return;

                if (timer >= attackTime)
                {
                    Shoot();
                    timer = 0.0f;
                }
            }
            else
            {
                timer = 0.0f;
                animator.SetBool("Shoot", false);
            }
        }
        else
        {
            timer = 0.0f;
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
            rb.velocity = Vector3.zero;
            mAgent.Stop();
        }
    }

    public void Shoot()
    {
        if(enemyGun && target) enemyGun.Shoot(target);
    }

    public void Restore()
    {
        health = maxHealth;
        isDeath = false;
        enemyGun.Restore();
        mAgent.isStopped = false;
    }

    protected virtual void Move()
    {
        float vel = mAgent.velocity.magnitude;
        animator.SetFloat("Speed", vel);
        if (isDetected)
        {
            if (Vector3.Distance(transform.position, target.position) > attackRange)
            {
                mAgent.isStopped = false;
                mAgent.SetDestination(target.position);
            }
            else
            {
                mAgent.isStopped = true;
                mAgent.velocity = Vector3.zero;
            }
        }
    }

    protected virtual IEnumerator Death()
    {
        isDeath = true;
        animator.SetBool("Shoot", false);
        mAgent.isStopped = true;
        yield return new WaitForSeconds(3.0f);
        ServiceLocator.Get<ObjectPoolManager>().RecycleObject(this.gameObject);
    }
}
