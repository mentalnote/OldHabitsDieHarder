using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    static public Weapons GetWeaponType(GameObject o)
    {
        Weapon w = o.GetComponentInChildren<Weapon>();
        if (w != null || (w = o.GetComponentInParent<Weapon>()) != null) {
            return w.weaponType;
        }

        //Not a weapon
        return Weapons.None;
    }

    [SerializeField]
    private Weapons weaponType = Weapons.None;

    [SerializeField]
    private Collider collider;

    [SerializeField]
    private Animation animation;

    [SerializeField]
    private AnimationClip animClip;

    [SerializeField]
    private bool disable = false;

    public Weapons WeaponType
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
        //this.soundSource.Play();
        this.StartPlayWeaponAnim();
    }

    public virtual void EndUseWeapon()
    {
        this.StopPlayWeaponAnim();
        //this.soundSource.Stop();
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
