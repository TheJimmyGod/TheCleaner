using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyGunner : Enemy
{
    protected override IEnumerator Death()
    {
        base.Death();
        GameObject gun = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool("HeavyGunnerGun");
        yield return null;
    }
}
