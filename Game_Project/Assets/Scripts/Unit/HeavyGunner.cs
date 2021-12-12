using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeavyGunner : Enemy
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if(isDetected)
        {
            Vector3 toTarget = target.position - transform.position;
            toTarget.y = 0.0f;
            Quaternion toRotation = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation * new Quaternion(0.0f,0.5f,0.0f,1.0f), 4.0f * Time.deltaTime);
        }
    }

    protected override IEnumerator Death()
    {
        StartCoroutine(base.Death());
        GameObject gun = GameObject.Instantiate(dropItem, transform.position + new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
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
