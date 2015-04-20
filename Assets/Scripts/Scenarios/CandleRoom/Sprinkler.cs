using UnityEngine;
using System.Collections;

public class Sprinkler : MonoBehaviour
{
	//The sprinkler system particle emitter prefab
	public GameObject emitter;

	void Start()
	{
		//When both of the curtains are on fire, start the sprinkler
		ScenarioManager.GetCurrentScenario().RegisterFlagTrigger(
			EnumUtil.ValuesToArray(new [] { CandleRoomScenario.Flags.LeftCurtainOnFire, CandleRoomScenario.Flags.RightCurtainOnFire }),
			new int[] {},
			new FlagTriggerDelegate(this.OnCurtainsOnFire),
			true  //isOneShot
		);
	}

	private void OnCurtainsOnFire(int cause)
	{
		//Start the sprinkler
        ScenarioManager.GetCurrentScenario().SetFlag(cause, CandleRoomScenario.Flags.SprinklersOn, true);
		GameObject.Instantiate(this.emitter);
	}
}
