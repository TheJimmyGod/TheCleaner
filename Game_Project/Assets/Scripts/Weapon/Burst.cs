using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : Weapon
{
    [SerializeField]
    public Transform firePoint2, firePoint3;
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override public void Shoot()
    {
        ShootForward();
        ShootRight();
        ShootLeft();
    }
    private void ShootForward()
    {
        if (ammo == 0) return;
        GameObject bullet = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(bulletName);
        bullet.SetActive(true);
        bullet.transform.position = firePoint.transform.position;
        bullet.GetComponent<Bullet>().Initialize(damage);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(firePoint.forward * attackSpeed, ForceMode.Impulse);
        particle.Play();
        particle_Sec.Play();
        ammo--;
    }
    private void ShootRight()
    {
        if (ammo == 0) return;
        GameObject bullet = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(bulletName);
        bullet.SetActive(true);
        bullet.transform.position = firePoint.transform.position;
        bullet.GetComponent<Bullet>().Initialize(damage);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        //rb.AddForce((firePoint.forward+new Vector3(0.0f,extentedRange,0.0f)  ) * attackSpeed, ForceMode.Impulse);
        rb.AddForce(firePoint2.forward * attackSpeed, ForceMode.Impulse);
        ammo--;
    }
    private void ShootLeft()
    {
        if (ammo == 0) return;
        GameObject bullet = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(bulletName);
        bullet.SetActive(true);
        bullet.transform.position = firePoint.transform.position ;
        bullet.GetComponent<Bullet>().Initialize(damage);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        //rb.AddForce((firePoint.forward + new Vector3(0.0f,-extentedRange,  0.0f)) * attackSpeed, ForceMode.Impulse);
        rb.AddForce(firePoint3.forward  * attackSpeed, ForceMode.Impulse);
        ammo--;
    }
}
