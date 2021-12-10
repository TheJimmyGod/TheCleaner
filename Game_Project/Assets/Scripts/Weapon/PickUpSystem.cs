using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    private static PickUpSystem _instance;

    public static PickUpSystem Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        pickups = new List<IPickUpable>();
    }
    [SerializeField]
    List<IPickUpable> pickups;

    public IPickUpable GetNearestPickUpObject(Vector3 from, out float minDis)
    {
        float min = float.MaxValue;
        IPickUpable nearestPickUpObject = pickups[0];
        foreach(IPickUpable pickUp in pickups)
        {
            float distance = Vector3.Distance(pickUp.transform.position, from);
            if (distance < min)
            {
                nearestPickUpObject = pickUp;
                min = distance;
            }
        }
        minDis = min;
        return nearestPickUpObject;
    }
    public void Register(IPickUpable pickup)
    {
        pickups.Add(pickup);
    }

    public void Unregister(IPickUpable pickup)
    {
        pickups.Remove(pickup);
    }
}
