using UnityEngine;
using System.Collections;

public class TargetZone : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		//If the table entered the target zone, set the flag accordingly
		if (other.gameObject.tag == "Table") {
			ScenarioManager.GetCurrentScenario().SetFlag(CandleRoomScenario.Flags.TableInPosition, true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		//If the table left the target zone, set the flag accordingly
		if (other.gameObject.tag == "Table") {
			ScenarioManager.GetCurrentScenario().SetFlag(CandleRoomScenario.Flags.TableInPosition, false);
		}
	}
}
