using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGunner : Enemy
{
    protected override IEnumerator Death()
    {
        StartCoroutine(base.Death());
        GameObject gun = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool("SniperGunnerGun");
        yield return null;
    }

    protected override void Move()
    {
        // Fixed position to shoot
    }
}
