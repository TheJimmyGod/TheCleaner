using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : Environment
{
    protected override IEnumerator DestroyObject()
    {
        StartCoroutine(base.DestroyObject());
        // TODO: Glass animation
        yield return null;
    }

    protected override void Interact()
    {
        base.Interact();
        // TODO: Glass Animation
        TakeDamage(health);
    }
}
