﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour,IPickUpable, IDropable
{
    public Transform firePoint;

    //private Rigidbody rb;
    [SerializeField]
    private string bulletName;
    [SerializeField]
    private float damage=0.0f;
    [SerializeField]
    private float force=0.0f;
    [SerializeField]
    private float attackSpeed=0.0f;

    Rigidbody rb ;
    [SerializeField]
    private int maxAmmo;
    private int ammo;
    // Start is called before the first frame update
    virtual protected void Start() 
    {
        PickUpSystem.Instance.Register(this);
        rb = gameObject.GetComponent<Rigidbody>();
        ammo = maxAmmo;
    }
    public void Restore()
    {
        ammo = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        if (ammo == 0) return;
        GameObject bullet = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(bulletName);
        bullet.SetActive(true);
        bullet.transform.position = firePoint.transform.position;
        bullet.GetComponent<Bullet>().Initialize(damage);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(firePoint.forward * attackSpeed, ForceMode.Impulse);
        ammo--;
    }
    void IPickUpable.PickUp(Transform parent)
    {
        Vector3 gunPos = new Vector3(0.59f, -0.05f, 0.65f);
        transform.localPosition =gunPos;
        rb.isKinematic = true;
        transform.SetParent(parent, false);
    }

    void IDropable.Drop(Transform parent)
    {
        transform.SetParent(parent,true);
        rb.isKinematic = false;
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        rb.AddForce(transform.up * force, ForceMode.Impulse);
        float ramdom = Random.Range(-1.0f, 0.0f);
        rb.AddTorque(new Vector3(ramdom, ramdom, ramdom));
    }

    T IPickUpable.GetClass<T>() 
    {
        return this.gameObject.GetComponentInParent<T>();
    }
}
