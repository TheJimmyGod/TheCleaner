using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestory : MonoBehaviour
{
    [SerializeField]
    public float mDelay = 2.0f;
    void Awake()
    {
        StartCoroutine("DelayedDestroyObject");
    }

    private IEnumerator DelayedDestroyObject()
    {
        yield return new WaitForSeconds(mDelay);
        ServiceLocator.Get<ObjectPoolManager>().RecycleObject(this.gameObject);
    }
}
