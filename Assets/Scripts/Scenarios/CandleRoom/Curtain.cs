using UnityEngine;
using System.Collections;

public class Curtain : MonoBehaviour
{
	//Which curtain flag we set
	public CandleRoomScenario.Flags curtainFlag;

	void OnCollisionEnter(Collision collision)
	{
		if (Weapon.GetWeaponType(collision.gameObject) == Weapons.FlameThrower)
		{
			//Remove the RigidBody from the fire that hit the curtain
			Destroy(collision.gameObject.GetComponent<Rigidbody>());
			Destroy(collision.collider);
			
			//Set the flag to indicate this curtain is on fire
            ScenarioManager.GetCurrentScenario().SetFlag(Weapons.FlameThrower, this.curtainFlag, true);
		}
	}
}
