using UnityEngine;
using System.Collections;

public class BabyRage : MonoBehaviour {

    [SerializeField]
    private BabyFood babyFood = null;

    [SerializeField]
    private AudioSource audio = null;

    [SerializeField]
    private Animation anim;

    [SerializeField]
    private AnimationClip animClip;

    private void Start()
    {
        if (this.anim != null && this.animClip != null)
        {
            this.anim[this.animClip.name].wrapMode = WrapMode.Loop;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Weapons hitBy = Weapon.GetWeaponType(collision.gameObject);

        if (hitBy != Weapons.None)
        {
            if (hitBy == Weapons.Bottle && this.babyFood != null && this.babyFood.AttachedToWeapon != null && this.babyFood.AttachedToWeapon.WeaponType == Weapons.Bottle)
            {
                ScenarioManager.GetCurrentScenario().SetFlag<Weapons, BabyScenario.Flags>(Weapons.None, BabyScenario.Flags.BabyFed, true);
                Destroy(this.babyFood.gameObject);
            }
            else
            {
                ScenarioManager.GetCurrentScenario().SetFlag<Weapons, BabyScenario.Flags>(hitBy, BabyScenario.Flags.BabyPopped, true);
                if (this.anim != null)
                {
                    this.audio.Stop();

                    this.anim.Stop();
                    Destroy(this.anim);
                }
            }
        }
    }
}
