using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGunner : Enemy
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if(isDetected)
        {
            Vector3 toTarget = target.position - transform.position;
            toTarget.y = 0.0f;
            Quaternion toRotation = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation * new Quaternion(0.0f, 0.5f, 0.0f, 1.0f), 4.0f * Time.deltaTime);
        }
    }
    protected override IEnumerator Death()
    {
        StartCoroutine(base.Death());
       // GameObject gun = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool("SniperGunnerGun");
        yield return null;
    }

    protected override void Move()
    {
        // Fixed position to shoot
    }
}
