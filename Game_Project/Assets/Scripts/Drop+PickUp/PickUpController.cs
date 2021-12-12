using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private KeyCode keyPickUpNDrop = KeyCode.F ;
    private PlayerController playerController;


    [SerializeField]
    private float deteachedRadius=0.0f;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyPickUpNDrop)&&!playerController.CurrentWeapon)
		{
			float distance;
			var obj = PickUpSystem.Instance.GetNearestPickUpObject(transform.position, out distance);
			if (obj.GetClass<Weapon>() && distance < deteachedRadius)
			{
				obj.PickUp(transform);
				playerController.CurrentWeapon = obj.GetClass<Weapon>();
				PickUpSystem.Instance.Register(obj);
			}
		}
		else if (Input.GetKeyDown(keyPickUpNDrop) && playerController.CurrentWeapon)
		{
			transform.Find("FPS Camera").GetComponent<MouseLook>().weapon = false;
			playerController.CurrentWeapon.GetComponent<IDropable>().Drop(PickUpSystem.Instance.gameObject.transform);
			PickUpSystem.Instance.Unregister(playerController.CurrentWeapon.GetComponent<IPickUpable>());
			playerController.CurrentWeapon = null;
		}
	}
}
