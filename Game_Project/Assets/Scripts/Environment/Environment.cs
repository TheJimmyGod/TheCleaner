using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected float maxHealth = 5.0f;
    protected float health = 0.0f;
    protected bool isDead = false;

    [SerializeField]
    private float interactDistance = 5.0f;
    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    void Update() => OnUpdate();

    protected virtual void OnUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) && (Vector3.Distance(player.transform.position, transform.position) <= interactDistance))
        {
            Interact();
        }
    }

    protected virtual void Interact()
    {
        Debug.Log("Do something!");
    }

    protected virtual IEnumerator DestroyObject()
    {
        isDead = true;
        Destroy(gameObject,1.1f);
        yield return null;
    }

    public void TakeDamage(float value)
    {
        if(!isDead)
        {
            health -= value;
            if(health <= 0.0f)
                StartCoroutine(DestroyObject());
        }
    }
}
