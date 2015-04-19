using UnityEngine;
using System.Collections;

public class Curtain : MonoBehaviour
{
	//Which curtain flag we set
	public CandleRoomScenario.Flags curtainFlag;

	void OnCollisionEnter(Collision collision)
	{
		if (Weapon.GetWeaponType(collision.gameObject) == "FIRE")
		{
			//Remove the RigidBody from the fire that hit the curtain
			Destroy(collision.gameObject.GetComponent<Rigidbody>());
			Destroy(collision.collider);
			
			//Set the flag to indicate this curtain is on fire
			ScenarioManager.GetCurrentScenario().SetFlag(this.curtainFlag);
		}
	}
}
