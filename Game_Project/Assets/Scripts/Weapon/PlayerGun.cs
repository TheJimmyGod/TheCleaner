using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public Transform mFirePoint;
    [SerializeField]
    private float mBulletSpeed = 100.0f;
    [SerializeField]
    private float mBulletDamage = 5.0f;

    public void Shoot()
    {
        GameObject bullet = ServiceLocator.Get<ObjectPoolManager>().GetObjectFromPool("PlayerBullet");
        bullet.SetActive(true);
        bullet.transform.position = mFirePoint.transform.position;
        bullet.GetComponent<Bullet>().Initialize(mBulletDamage);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(mFirePoint.forward * mBulletSpeed, ForceMode.Impulse);
    }
}
