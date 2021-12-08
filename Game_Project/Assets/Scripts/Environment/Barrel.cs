using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : Environment
{
    [SerializeField]
    private float explosionRange = 0.0f;
    [SerializeField]
    private float damage = 1.0f;
    protected override IEnumerator DestroyObject()
    {
        StartCoroutine(base.DestroyObject());
        // TODO: Spawn explosion effect
        Collider[] collider = Physics.OverlapSphere(transform.localPosition, explosionRange);
        if (collider.Length == 0)
            yield return null;
        else
        {
            for (int i = 0; i < collider.Length; i++)
            {
                var unit = collider[i].transform.GetComponent<IDamagable>();
                unit.TakeDamage(damage);
            }
        }
        yield return null;
    }
}
