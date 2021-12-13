using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    public bool autoGun = false;

    public GameObject owner;
    public Animator animator;
    public ParticleSystem particle;
    public ParticleSystem particle_Sec;
    public AudioClip gunSFX;

    void Start()
    {
        ammo = maxAmmo;
        animator = GetComponent<Animator>();
        gunSFX = GetComponent<AudioClip>();
    }

    public void Restore()
    {
        ammo = maxAmmo;
    }

    private void Update()
    {
        if (gunSFX == null)
            gunSFX = owner.GetComponent<Enemy>().clip;
        if(owner.GetComponent<Enemy>().isDead)
            animator.Play("Idle");
        if(owner.GetComponent<Enemy>().IsDetected == false && autoGun)
        {
            particle.loop = false;
            particle_Sec.loop = false;
        }
    }

    public void StopEffect()
    {
        particle.Stop();
        particle_Sec.Stop();
    }

    public void Shoot(Transform target)
    {
        if (ammo == 0) return;
        animator?.Play("Fire");
        GameObject bullet = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool(bulletName);
        bullet.transform.position = firePoint.transform.position;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().Initialize(bulletDamage);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        bullet.transform.rotation = Quaternion.LookRotation(firePoint.position + rb.velocity);
        if(autoGun)
        {
            particle.loop = true;
            particle_Sec.loop = true;
        }
        particle.Play();
        particle_Sec.Play();
        ServiceLocator.Get<AudioManager>().PlaySfx(gunSFX);
        //TODO: Input sound clip
        ammo--;
    }
}
