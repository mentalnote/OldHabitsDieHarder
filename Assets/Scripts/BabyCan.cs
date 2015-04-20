using UnityEngine;
using System.Collections;

public class BabyCan : MonoBehaviour {

    [SerializeField]
    private Animation anim;

    [SerializeField]
    private AnimationClip animClip;

    private bool isOpened = false;

    private void OnCollisionEnter(Collision collision)
    {
        Weapons hitBy = Weapon.GetWeaponType(collision.gameObject);

        if (this.isOpened && hitBy == Weapons.Bottle && collision.transform != this.transform.parent)
        {
            this.transform.parent = collision.transform;
            this.transform.position = collision.contacts[0].point;
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
