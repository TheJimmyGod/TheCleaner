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

    public GameObject playerGun;
    public Player player;

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
            if (playerGun == null)
                Debug.Log("Gun is not available!");
            else
                playerGun.gameObject.GetComponent<Gun>().Shoot();
        }

        mVelocity.y += mGravity * Time.deltaTime;

        mBodyController.Move(mVelocity * Time.deltaTime);
    }
}
