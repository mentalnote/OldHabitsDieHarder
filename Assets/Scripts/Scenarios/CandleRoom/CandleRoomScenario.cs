using UnityEngine;
using System.Collections;

public class CandleRoomScenario : Scenario
{
	
	//Our flags enum
	public enum Flags
	{
		LeftCurtainOnFire,
		RightCurtainOnFire,
		SprinklersOn,
        ChairDestroyed,
		TableInPosition,
        CandleDoused,
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

        this.RegisterFlagTrigger(
            EnumUtil.ValuesToArray(new[] { 
                Flags.LeftCurtainOnFire,
                Flags.RightCurtainOnFire,
                Flags.SprinklersOn,
                Flags.ChairDestroyed,
                Flags.TableInPosition,
                Flags.CandleDoused
            }),
            new int[] { },
            new FlagTriggerDelegate(this.OnCandleDoused),
            true  //isOneShot
        );
	}
	
	private void OnCandleDestroyed(int cause)
	{
		Debug.Log("YOU DESTROYED THE CANDLE!");
		Debug.Log ("CURRENT SCENARIO: " + ScenarioManager.GetCurrentScenario());
		ScenarioManager.GetCurrentScenario().LoseScenario((Weapons)cause);
	}

    private void OnCandleDoused(int cause)
    {
        Debug.Log("YOU DOUSED THE CANDLE!");
        Debug.Log("CURRENT SCENARIO: " + ScenarioManager.GetCurrentScenario());
        ScenarioManager.GetCurrentScenario().WinScenario();
    }
}
