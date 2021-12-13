using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController mBodyController;

    private Vector3 mVelocity;

    [SerializeField]
    private float mSpeed = 12.0f;
    [SerializeField]
    private float mGravity = -9.81f;
    [SerializeField]
    private float mJumpHeight = 2.0f;

    public Transform mGroundCheck;
    [SerializeField]
    private float mGroundDistance = 0.4f;
    public LayerMask mGroundMask;

    private bool isGrounded;
    public LayerMask enemyLayer;
    public GameObject playerGun;
    public Player player;

    public AudioClip punch;
    private Weapon currentWeapon;
    
    public Weapon CurrentWeapon 
    { get => currentWeapon; set => currentWeapon = value; }

    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(mGroundCheck.position, mGroundDistance, mGroundMask);

        if(isGrounded && mVelocity.y < 0.0f)
            mVelocity.y = -2.0f;

        if (player.gameObject.GetComponent<Player>().IsDead)
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right* x + transform.forward * z;

        mBodyController.Move(movement * mSpeed * Time.deltaTime);

        if(isGrounded && Input.GetButtonDown("Jump"))
            mVelocity.y = Mathf.Sqrt(mJumpHeight * -2.0f * mGravity);
        if(Input.GetMouseButtonDown(0))
        {
            if (currentWeapon == null)
            {
                Collider[] collider = Physics.OverlapSphere(transform.localPosition, 5.0f, enemyLayer);
                if(collider.Length != 0)
                {
                    float maxFloat = float.MaxValue;
                    float dist = 0.0f;
                    IDamagable damagable = null;
                    for (int i = 0; i < collider.Length; ++i)
                    {
                        var unit = collider[i].transform;
                        dist = Vector3.Distance(transform.position, unit.position);
                        if(maxFloat > dist)
                        {
                            maxFloat = dist;
                            if(unit.GetComponent<Enemy>().isDead == false)
                                damagable = unit.GetComponent<IDamagable>();
                        }
                    }
                    if (damagable != null)
                    {
                        ServiceLocator.Get<AudioManager>().PlaySfx(punch);
                        damagable.TakeDamage(10);
                    }
                }
            }
            else
                currentWeapon.Shoot();
        }

        mVelocity.y += mGravity * Time.deltaTime;

        mBodyController.Move(mVelocity * Time.deltaTime);
    }
}
