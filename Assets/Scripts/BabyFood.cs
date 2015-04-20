using UnityEngine;
using System.Collections;

public class BabyFood : MonoBehaviour {

    [SerializeField]
    private CharacterController playerCollider = null;

    public bool IsAttached { get; set; }

    public Transform AttachedTo { get; set; }

    public Weapon AttachedToWeapon { get; set; }

    private void Update()
    {
        if (this.IsAttached && this.AttachedTo != null)
        {
            Physics.IgnoreCollision(this.playerCollider, this.GetComponent<Collider>());

            this.transform.position = this.AttachedTo.position + this.AttachedTo.forward * 0.1f ;
        }

        if (IsAttached && AttachedToWeapon == null)
        {
            ScenarioManager.GetCurrentScenario().SetFlag<Weapons, BabyScenario.Flags>(Weapons.Bottle, BabyScenario.Flags.FoodDestroyed, true);
            Destroy(this.gameObject);
        }
    }
}
