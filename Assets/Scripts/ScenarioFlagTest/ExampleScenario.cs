using UnityEngine;
using System.Collections;

public class ExampleScenario : Scenario
{
	//Returns the narration clip for when we win the scenario
	protected override Narration getScenarioWonNarration()  { return Narration.Test1; }
	
	//Returns the narration clip for when we lose the scenario
	protected override Narration getScenarioLostNarration() { return Narration.Test2; }

	//Our flags enum
	public enum Flags
	{
		FlagOne,
		FlagTwo,
		FlagThree,
		FlagFour,
		FlagFive
	};

	//We need to implement this to tell the base class about our flag enum values
	override protected int[] GetFlagKeys() {
		return EnumUtil.ArrayFromEnum(typeof(ExampleScenario.Flags));
	}
	
	void Start()
	{
		//We need to call InitialiseScenario() in either Awake() or Start()
		this.InitialiseScenario();

		//Register our flag triggers
		this.RegisterFlagTrigger(
			EnumUtil.ValuesToArray(new [] { Flags.FlagOne, Flags.FlagTwo }),
			new int[] {},
			new FlagTriggerDelegate(this.OnFirstTwoFlagsSet)
		);
		this.RegisterFlagTrigger(
			EnumUtil.ValuesToArray(new [] { Flags.FlagOne, Flags.FlagFive }),
			new int[] {},
			new FlagTriggerDelegate(this.OnFirstAndLastFlagsAreSet),
			true  //isOneShot
		);
		this.RegisterFlagTrigger(
			EnumUtil.ValuesToArray(new [] { Flags.FlagTwo }),
			EnumUtil.ValuesToArray(new [] { Flags.FlagFour }),
			new FlagTriggerDelegate(this.OnSecondFlagIsSetButFourthFlagIsNot)
		);
	}
	
	private void OnFirstTwoFlagsSet()
	{
		Debug.Log(System.DateTime.Now + " THE FIRST TWO FLAGS ARE SET!");
	}

	private void OnFirstAndLastFlagsAreSet()
	{
		Debug.Log("THE FIRST AND THE FIFTH FLAGS ARE SET!");
	}

	private void OnSecondFlagIsSetButFourthFlagIsNot()
	{
		Debug.Log("THE SECOND FLAG IS SET WHILE THE FOURTH IS NOT!");
	}
}
