using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    static public string GetWeaponType(GameObject o)
    {
        Weapon w = o.GetComponent<Weapon>();
        if (w != null) {
            return w.weaponType;
        }

        //Not a weapon
        return "";
    }

    [SerializeField]
    private string weaponType = "BASE";

    [SerializeField]
    private Collider collider;

    [SerializeField]
    private Animation animation;

    [SerializeField]
    private AnimationClip animClip;

    [SerializeField]
    private bool disable = false;

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
        if (this.animation != null && this.animClip != null)
        {
            this.animation.clip = this.animClip;
            this.animation.Play(this.animation.clip.name);
            this.animation[this.animation.clip.name].wrapMode = WrapMode.Loop;
        }
    }

    protected void StopPlayWeaponAnim()
    {
        if (this.animation != null && this.animClip != null)
        {
            this.animation[this.animation.clip.name].wrapMode = WrapMode.Once;
        }
    }

    private void Start()
    {
        if (!disable)
        {
            this.ColliderEnabled = false;    
        }
    }
}
