using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPickUpable
{
    void PickUp(Transform parent);
    Transform transform { get; }
    T GetClass<T>();
}
