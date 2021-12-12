using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Environment
{
    private float startRotation = 0.0f;
    private float endRotation = 0.0f;
    [SerializeField]
    private float speed = 3.0f;

    public Transform hingePoint;
    private bool isOpen = false;
    private float current = 0.0f;
    private void Start()
    {
        startRotation = transform.eulerAngles.y;
        endRotation = startRotation + 120.0f;
    }
    protected override void Interact()
    {
        base.Interact();
        if(!isOpen)
        {
            isOpen = true;
            StopCoroutine(CloseDoor());
            StartCoroutine(OpenDoor());
        }
        else
        {
            isOpen = false;
            StopCoroutine(OpenDoor());
            StartCoroutine(CloseDoor());
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    private IEnumerator OpenDoor()
    {
        Debug.Log("Open");
        current = transform.eulerAngles.y;
        while (current < endRotation)
        {
            current += speed * Time.deltaTime;
            transform.RotateAround(hingePoint.transform.position, Vector3.up, Time.deltaTime * speed);
            if (current >= endRotation)
                transform.rotation = Quaternion.Euler(0.0f, endRotation, 0.0f);
            yield return null;
        }
    }

    private IEnumerator CloseDoor()
    {
        Debug.Log("Close");
        current = endRotation;
        while (current > startRotation)
        {
            current += -speed * Time.deltaTime;
            transform.RotateAround(hingePoint.transform.position, Vector3.up, -Time.deltaTime * speed);
            if (current <= startRotation)
                transform.rotation = Quaternion.Euler(0.0f, startRotation, 0.0f);
            yield return null;
        }
        
    }
}
