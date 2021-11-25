using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public float mLifeSpan = 2.0f;
    public float mDamage = 5.0f;
    private bool isInitialized = false;

    public void Initialize(float damage)
    {
        mDamage = damage;
        StartCoroutine("DelayedDestroyObject");
        isInitialized = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isInitialized == false)
            return;
        var damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
            damagable.TakeDamage(mDamage);
    }

    private IEnumerator DelayedDestroyObject()
    {
        yield return new WaitForSeconds(mLifeSpan);
        ServiceLocator.Get<ObjectPoolManager>().RecycleObject(this.gameObject);
    }

    private void OnDisable()
    {
        isInitialized = false;
    }
}
