using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 100.0f;

    public Transform controllerBody;
    public Transform controllerGun;

    public Player player;

    private float xRot = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent.gameObject.GetComponent<Player>();
    }

    void Update()
    {
        if (player.IsDead)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);
        controllerBody.Rotate(Vector3.up * mouseX);
        controllerGun.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);
    }
}
