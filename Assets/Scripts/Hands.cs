using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.FirstPerson;

public class Hands : MonoBehaviour
{
    [SerializeField]
    private GameObject handObject;

    [SerializeField]
    private FirstPersonController fpsController;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float heldWeaponOffset = 1.0f;

    private Weapon heldWeapon;

    public Vector3 Position { get; set; }

    private float weaponUseMinimumTime = 0.0f;

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
                Destroy(this.heldWeapon.gameObject);
            }

            if (value == null)
            {
                return;
            }

            GameObject weaponGameObject = Instantiate(value.transform.root.gameObject);

            weaponGameObject.transform.parent = this.handObject.transform;
            this.handObject.transform.position = this.Position;
            this.handObject.transform.rotation = Quaternion.LookRotation(this.transform.forward, Vector3.up);

            weaponGameObject.transform.localPosition = Vector3.zero;
            weaponGameObject.transform.localRotation = Quaternion.identity;

            this.heldWeapon = weaponGameObject.GetComponentInChildren<Weapon>();
        }
    }

	private void Update () {
        if (this.heldWeapon != null && this.fpsController.enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.heldWeapon.StartUseWeapon();

                this.weaponUseMinimumTime = 0.3f;
            }
            else if (this.weaponUseMinimumTime <= 0.0f && !Input.GetMouseButton(0))
            {
                this.heldWeapon.EndUseWeapon();
            }
        }

        if (this.weaponUseMinimumTime > 0.0f)
        {
            this.weaponUseMinimumTime -= Time.deltaTime;
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

        this.Position = this.camera.ScreenPointToRay(Input.mousePosition * 0.5f + new Vector3(Screen.width * 0.25f, Screen.height * 0.25f, 0.0f)).GetPoint(this.heldWeaponOffset);

        if (this.handObject != null)
        {
            this.heldWeapon.transform.position = this.Position;
            this.heldWeapon.transform.forward = camera.transform.forward;
        }
    }
}
