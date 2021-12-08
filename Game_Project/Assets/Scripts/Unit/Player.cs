using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private float mHealth = 0.0f;
    [SerializeField]
    private float mMaxHealth = 100.0f;

    public float Health { get { return mHealth; } }

    private bool isDead = false;

    private Bleeding bleedEffect;
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public void TakeDamage(float value)
    { 
        if(!isDead)
        {
            Debug.Log("Damaged!");
            mHealth -= value;
            bleedEffect.StartCoroutine("StartBleeding");
            if (mHealth <= 1)
            {
                bleedEffect.StartCoroutine("StartFatalBleeding");
            }
            if (mHealth <= 0.0f)
            {
                isDead = true;
                bleedEffect.StopAllCoroutines();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        mHealth = mMaxHealth;
        bleedEffect = transform.Find("PostProcessing").gameObject.GetComponent<Bleeding>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
