using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Weapon : MonoBehaviour
{
    [SerializeField]
    private string weaponType = "BASE";

    [SerializeField]
    private Collider collider;

    [SerializeField]
    private Animation animation;

    [SerializeField]
    private AnimationClip animClip;

    public string WeaponType
    {
        get
        {
            return this.weaponType;
        }

        set
        {
            this.weaponType = value;
        }
    }

    public bool ColliderEnabled
    {
        get
        {
            if (this.collider != null)
            {
                return this.collider.enabled;
            }

            return false;
        }

        set
        {
            if (this.collider != null)
            {
                this.collider.enabled = value;
            }
        }
    }

    public virtual void StartUseWeapon()
    {
        this.ColliderEnabled = true;
        this.StartPlayWeaponAnim();
    }

    public virtual void EndUseWeapon()
    {
        this.StopPlayWeaponAnim();
        this.ColliderEnabled = false;
    }

    protected void StartPlayWeaponAnim()
    {
        if (this.animation != null && !this.animation.isPlaying)
        {
            this.animation.clip = this.animClip;
            this.animation.Play();
        }
    }

    protected void StopPlayWeaponAnim()
    {
        if (this.animation != null && this.animation.isPlaying)
        {
            this.animation.Stop();
        }
    }

    private void Start()
    {
        this.ColliderEnabled = false;
    }
}
