using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.FirstPerson;

public class Hands : MonoBehaviour
{
    [SerializeField]
    private FirstPersonController fpsController;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float heldWeaponOffset = 10.0f;

    private Weapon heldWeapon;

    public Vector3 Position { get; set; }

    public Weapon HeldWeapon
    {
        get
        {
            return this.heldWeapon;
        }

        set
        {
            if (this.heldWeapon != null)
            {
                Destroy(this.heldWeapon);
            }

            if (value == null)
            {
                return;
            }

            GameObject weaponGameObject = (GameObject)Instantiate(
                value.transform.root.gameObject,
                this.Position,
                Quaternion.LookRotation(this.transform.forward, Vector3.up));

            weaponGameObject.transform.parent = this.transform;

            this.heldWeapon = weaponGameObject.GetComponentInChildren<Weapon>();
        }
    }

	private void Update () {
        if (this.heldWeapon != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.heldWeapon.StartUseWeapon();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                this.heldWeapon.EndUseWeapon();
            }
        }

	    if (Input.GetKeyDown(KeyCode.LeftShift))
	    {
	        this.fpsController.MouseLook.XSensitivity = 0f;
	        this.fpsController.MouseLook.YSensitivity = 0f;
	    }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            this.fpsController.MouseLook.XSensitivity = 2f;
            this.fpsController.MouseLook.YSensitivity = 2f;
        }

        this.Position = this.camera.ScreenPointToRay(Input.mousePosition).GetPoint(this.heldWeaponOffset);

	    this.heldWeapon.transform.position = this.Position;
	}
}
