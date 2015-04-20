using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
        Weapons weaponType = Weapon.GetWeaponType(collision.gameObject);
        if (weaponType != Weapons.None)
        {
            ScenarioManager.GetCurrentScenario().SetFlag(weaponType, CandleRoomScenario.Flags.CandleDestroyed, true);
		}
	}
}
