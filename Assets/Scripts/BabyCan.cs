using UnityEngine;
using System.Collections;

public class BabyCan : MonoBehaviour {

    [SerializeField]
    private Animation anim;

    [SerializeField]
    private AnimationClip animClip;

    [SerializeField]
    private BabyFood babyFood;

    private bool isOpened = false;





    private void OnCollisionEnter(Collision collision)
    {
        Weapons hitBy = Weapon.GetWeaponType(collision.gameObject);

        if (this.isOpened && hitBy == Weapons.Bottle && collision.transform != this.transform.parent)
        {
            Weapon weapon = collision.gameObject.GetComponentInChildren<Weapon>();
            if (weapon != null && this.babyFood != null)
            {
                this.babyFood.AttachedTo = weapon.transform;
                this.babyFood.IsAttached = true;
                this.babyFood.AttachedToWeapon = weapon;
            }
        }
        else if (!this.isOpened && hitBy == Weapons.Chainsaw)
        {
            this.isOpened = true;
            if (this.anim != null && this.animClip != null)
            {
                this.anim.clip = this.animClip;
                this.anim.Play(this.animClip.name);
                this.anim[this.animClip.name].wrapMode = WrapMode.Once;
            }
        }
        else if (hitBy != Weapons.Bottle && hitBy != Weapons.Chainsaw && hitBy != Weapons.None)
        {
            ScenarioManager.GetCurrentScenario().SetFlag<Weapons, BabyScenario.Flags>(hitBy, BabyScenario.Flags.FoodDestroyed, true);
        }
    }
}
