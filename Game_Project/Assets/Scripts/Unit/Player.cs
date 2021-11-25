using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private float mHealth = 0.0f;
    [SerializeField]
    private float mMaxHealth = 100.0f;

    private bool isDead = false;

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public void TakeDamage(float value)
    { 
        if(!isDead)
        {
            mHealth -= value;
            if (mHealth >= 0.0f)
                isDead = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        mHealth = mMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
