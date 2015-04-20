using UnityEngine;
using System.Collections;

public class Diaper : MonoBehaviour {

    [SerializeField]
    private Animation anim;

    [SerializeField]
    private AnimationClip animClip;

    [SerializeField]
    private AnimationClip idleAnimClip;

    private void OnCollisionEnter(Collision collision)
    {
        Weapons hitBy = Weapon.GetWeaponType(collision.gameObject);

        if (hitBy == Weapons.Knife)
        {
            ScenarioManager.GetCurrentScenario().SetFlag<Weapons, BabyScenario.Flags>(Weapons.Knife, BabyScenario.Flags.BabyChanged, true);
            if (this.anim != null && this.animClip != null)
            {
                this.anim.clip = this.animClip;
                this.anim.Play(this.animClip.name);
                this.anim[this.animClip.name].wrapMode = WrapMode.Once;

                if (this.idleAnimClip != null)
                {
                    this.anim.PlayQueued(this.idleAnimClip.name);
                    this.anim[this.idleAnimClip.name].wrapMode = WrapMode.Once;
                }
            }
            
        }
        else if (hitBy != Weapons.None)
        {
            ScenarioManager.GetCurrentScenario().SetFlag<Weapons, BabyScenario.Flags>(hitBy, BabyScenario.Flags.BabyPopped, true);
        }
    }
}
