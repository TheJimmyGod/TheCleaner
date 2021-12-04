using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootGunner : Enemy
{
    protected override IEnumerator Death()
    {
        base.Death();
        GameObject gun = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool("FootGunnerGun");
        yield return null;
    }

    protected override void Move()
    {
        base.Move();
        // TODO: Patrol
    }
}
