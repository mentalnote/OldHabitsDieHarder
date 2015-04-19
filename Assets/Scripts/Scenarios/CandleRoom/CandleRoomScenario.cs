using UnityEngine;
using System.Collections;

public class CandleRoomScenario : Scenario
{
	//Returns the narration clip for when we win the scenario
	protected override Narration getScenarioWonNarration()  { return Narration.Test1; }
	
	//Returns the narration clip for when we lose the scenario
	protected override Narration getScenarioLostNarration() { return Narration.Test2; }
	
	//Our flags enum
	public enum Flags
	{
		LeftCurtainOnFire,
		RightCurtainOnFire,
		SprinklersOn,
		TableInPosition,
		CandleDestroyed
	};

	//We need to implement this to tell the base class about our flag enum
	override protected System.Type GetEnumType() {
		return typeof(CandleRoomScenario.Flags);
	}

	void Awake()
	{
		//We need to call InitialiseScenario() in either Awake() or Start()
		this.InitialiseScenario();
		
		//Register our flag triggers

		//When the candle is destroyed, we lose the level
		this.RegisterFlagTrigger(
			EnumUtil.ValuesToArray(new [] { Flags.CandleDestroyed }),
			new int[] {},
			new FlagTriggerDelegate(this.OnCandleDestroyed),
			true  //isOneShot
		);
	}
	
	private void OnCandleDestroyed()
	{
		Debug.Log("YOU DESTROYED THE CANDLE!");
		Debug.Log ("CURRENT SCENARIO: " + ScenarioManager.GetCurrentScenario());
		ScenarioManager.GetCurrentScenario().LoseScenario();
	}
}
