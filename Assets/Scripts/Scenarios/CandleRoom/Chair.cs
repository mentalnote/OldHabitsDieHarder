using UnityEngine;
using System.Collections;

public class Chair : MonoBehaviour {
    //Which curtain flag we set
    public CandleRoomScenario.Flags curtainFlag;

    private bool broken = false;

    public bool Broken
    {
        get
        {
            return this.broken;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!broken && Weapon.GetWeaponType(collision.gameObject) == Weapons.Sword)
        {
            foreach (Transform child in this.transform)
            {
                child.gameObject.AddComponent<Rigidbody>();
            }

            this.transform.DetachChildren();

            broken = true;

            //Set the flag to indicate this curtain is on fire
            ScenarioManager.GetCurrentScenario().SetFlag(Weapons.Sword, this.curtainFlag, true);
        }
    }
}
