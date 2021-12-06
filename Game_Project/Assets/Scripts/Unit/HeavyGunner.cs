using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeavyGunner : Enemy
{
    protected override IEnumerator Death()
    {
        StartCoroutine(base.Death());
        GameObject gun = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool("HeavyGunnerGun");
        yield return null;
    }
    protected override void Move()
    {
        base.Move();
        if (wonderTimer >= wonderLimit)
        {
            var randomDirection = Random.insideUnitSphere * 20.0f;
            randomDirection += transform.position;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, 20.0f, -1);
            mAgent.SetDestination(navHit.position);
            wonderTimer = 0.0f;
        }
    }
}
