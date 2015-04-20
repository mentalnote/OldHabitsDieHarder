using UnityEngine;
using System.Collections;

public class DogRoomScenario : Scenario
{
	
	//Our flags enum
	public enum Flags
	{
		BoardRetrieved,
		PoopCaught,
		BoardKnockedAway,
		DogDestroyed,
        PoopMissed
	};

	//We need to implement this to tell the base class about our flag enum
	override protected System.Type GetEnumType() {
        return typeof(DogRoomScenario.Flags);
	}

	void Awake()
	{
		//We need to call InitialiseScenario() in either Awake() or Start()
		this.InitialiseScenario();
		
		//Register our flag triggers

		//When the dog is destroyed, we lose the level
		this.RegisterFlagTrigger(
			EnumUtil.ValuesToArray(new [] { Flags.DogDestroyed }),
			new int[] {},
			new FlagTriggerDelegate(this.OnDogDestroyed),
			true  //isOneShot
		);

        //When the poop is missed, we lose the level
        this.RegisterFlagTrigger(
            EnumUtil.ValuesToArray(new[] { Flags.PoopMissed }),
            new int[] { },
            new FlagTriggerDelegate(this.OnPoopMissed),
            true  //isOneShot
        );

        this.RegisterFlagTrigger(
            EnumUtil.ValuesToArray(new[] { 
                Flags.BoardRetrieved,
                Flags.PoopCaught,
                Flags.BoardKnockedAway
            }),
            new int[] { },
            new FlagTriggerDelegate(this.OnDogRescued),
            true  //isOneShot
        );
	}

    private void OnDogDestroyed(int cause)
	{
		Debug.Log("YOU DESTROYED THE DOG!");
		Debug.Log ("CURRENT SCENARIO: " + ScenarioManager.GetCurrentScenario());
		ScenarioManager.GetCurrentScenario().LoseScenario((Weapons)cause);
	}

    private void OnPoopMissed(int cause)
    {
        Debug.Log("YOU MISSED THE POOP!");
        Debug.Log("CURRENT SCENARIO: " + ScenarioManager.GetCurrentScenario());
        ScenarioManager.GetCurrentScenario().LoseScenario((Weapons)cause);
    }

    private void OnDogRescued(int cause)
    {
        Debug.Log("YOU RESCUED THE DOG!");
        Debug.Log("CURRENT SCENARIO: " + ScenarioManager.GetCurrentScenario());
        ScenarioManager.GetCurrentScenario().WinScenario();
    }
}
