using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float mLifeSpan = 2.0f;
    [SerializeField]
    private float mDamage = 5.0f;
    [SerializeField]
    private float mAccurancy = 0.1f;

    [SerializeField]
    private float mHeadShotDamage = 4.0f;
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

        var isPlayer = collision.gameObject.GetComponent<Player>();
        if (isPlayer != null)
        {
            if (Random.value < mAccurancy)
                isPlayer.TakeDamage(mDamage);
        }
        else
        {
            Debug.Log(collision.gameObject.name);
            if(collision.gameObject.name == "Head")
            {
                Debug.Log("HeadShot");
                var headShot = collision.transform.parent.gameObject.GetComponent<IDamagable>();
                if(headShot != null)
                    headShot.TakeDamage(mHeadShotDamage);
            }
            else
            {
                Debug.Log("BodyShot");
                var bodyshot = collision.gameObject.GetComponent<IDamagable>();
                if (bodyshot != null)
                    bodyshot.TakeDamage(mDamage);
            }
        }
        ServiceLocator.Get<ObjectPoolManager>().RecycleObject(this.gameObject);
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
