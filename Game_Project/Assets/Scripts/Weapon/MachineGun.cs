using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    // Start is called before the first frame update
    public float delay=0.1f; // change
    public int shootAmount=5;
 
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
        StartCoroutine (ShootForward());
    }
    public IEnumerator ShootForward()
    {
        //if (ammo == 0) return;
        particle.loop = true;
        particle_Sec.loop = true;
        particle.Play();
        particle_Sec.Play();
        ServiceLocator.Get<AudioManager>().PlaySfx(gunSFX);
        for (int i = 0; i <= shootAmount; )
        {
            yield return new WaitForSeconds(delay);
            GameObject bullet = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(bulletName);
            bullet.SetActive(true);
            bullet.transform.position = firePoint.transform.position;
            bullet.GetComponent<Bullet>().Initialize(damage);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(firePoint.forward * attackSpeed, ForceMode.Impulse);
            ammo--;
            ++i;
        }
        particle.loop = false;
        particle_Sec.loop = false;
    }



}
