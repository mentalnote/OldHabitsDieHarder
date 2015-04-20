using UnityEngine;
using System.Collections;

public class BabyCan : MonoBehaviour {

    [SerializeField]
    private Animation anim;

    [SerializeField]
    private AnimationClip animClip;

    private bool isOpened = false;

    private bool isAttached = false;

    private Transform attachedTo;

    private void Update()
    {
        if (this.isAttached && this.attachedTo != null)
        {
            this.transform.position = this.attachedTo.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Weapons hitBy = Weapon.GetWeaponType(collision.gameObject);

        if (this.isOpened && hitBy == Weapons.Bottle && collision.transform != this.transform.parent)
        {
            Weapon weapon = collision.gameObject.GetComponentInChildren<Weapon>();
            if (weapon != null)
            {
                this.attachedTo = weapon.transform;
                this.isAttached = true;
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
        else
        {
            BabyRage babyRage = collision.gameObject.GetComponent<BabyRage>();
            if (babyRage != null)
            {
                ScenarioManager.GetCurrentScenario().SetFlag<Weapons, BabyScenario.Flags>(Weapons.None, BabyScenario.Flags.BabyFed, true);
                Destroy(this.gameObject);
            }
        }
    }
}
