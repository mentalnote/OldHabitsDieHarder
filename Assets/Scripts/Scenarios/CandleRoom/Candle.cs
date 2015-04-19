using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		if (Weapon.GetWeaponType(collision.gameObject) != "") {
			ScenarioManager.GetCurrentScenario().SetFlag(CandleRoomScenario.Flags.CandleDestroyed);
		}
	}
}
