using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootGunner : Enemy
{

    protected override IEnumerator Death()
    {
        StartCoroutine(base.Death());
        yield return new WaitForSeconds(1.0f);
        GameObject gun = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool("FootGunnerGun");
    }

    protected override void Move()
    {
        base.Move();
        if(wonderTimer >= wonderLimit)
        {
            var randomDirection = Random.insideUnitSphere * 50.0f;
            randomDirection += transform.position;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, 50.0f, -1);
            mAgent.SetDestination(navHit.position);
            wonderTimer = 0.0f;
        }
    }
}
