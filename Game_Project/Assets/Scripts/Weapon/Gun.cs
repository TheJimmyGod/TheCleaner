using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    
    [SerializeField]
    private string bulletName;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float bulletDamage;

    [SerializeField]
    private int maxAmmo;
    private int ammo;

    void Start()
    {
        ammo = maxAmmo;
    }

    public void Shoot()
    {
        if (ammo == 0) return;
        GameObject bullet = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(bulletName);
        bullet.SetActive(true);
        bullet.transform.position = firePoint.transform.position;
        bullet.GetComponent<Bullet>().Initialize(bulletDamage);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(firePoint.forward * bulletDamage, ForceMode.Impulse);
        ammo--;
    }
}
