using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		if (Weapon.GetWeaponType(collision.gameObject) != "")
		{
			Debug.Log("YOU DESTROYED THE CANDLE!");
			Debug.Log ("CURRENT SCENARIO: " + ScenarioManager.GetCurrentScenario());
			ScenarioManager.GetCurrentScenario().LoseScenario();
		}
	}
}
