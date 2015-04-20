using UnityEngine;
using System.Collections;

public class BabyScenario : Scenario {

    //Our flags enum
    public enum Flags
    {
        BabyPopped,
        FoodDestroyed,
        BabyChanged,
        BabyFed
    };

    //We need to implement this to tell the base class about our flag enum
    override protected System.Type GetEnumType()
    {
        return typeof(BabyScenario.Flags);
    }

    void Awake()
    {
        //We need to call InitialiseScenario() in either Awake() or Start()
        this.InitialiseScenario();

        //Register our flag triggers

        //When the babt is destroyed, we lose the level
        this.RegisterFlagTrigger(
            EnumUtil.ValuesToArray(new[] { Flags.BabyPopped }),
            new int[] { },
            new FlagTriggerDelegate(this.OnBabyDestroyed),
            true  //isOneShot
        );

        //When the food is destroyed, we lose the level
        this.RegisterFlagTrigger(
            EnumUtil.ValuesToArray(new[] { Flags.FoodDestroyed }),
            new int[] { },
            new FlagTriggerDelegate(this.OnFoodDestroyed),
            true  //isOneShot
        );

        this.RegisterFlagTrigger(
            EnumUtil.ValuesToArray(new[] { 
                Flags.BabyChanged,
                Flags.BabyFed
            }),
            new int[] { },
            new FlagTriggerDelegate(this.OnBabyDoused),
            true  //isOneShot
        );
    }

    private void OnBabyDestroyed(int cause)
    {
        Debug.Log("YOU DESTROYED THE BABY!");
        Debug.Log("CURRENT SCENARIO: " + ScenarioManager.GetCurrentScenario());
        ScenarioManager.GetCurrentScenario().LoseScenario((Weapons)cause);
    }

    private void OnFoodDestroyed(int cause)
    {
        Debug.Log("YOU DESTROYED THE FOOD!");
        Debug.Log("CURRENT SCENARIO: " + ScenarioManager.GetCurrentScenario());
        ScenarioManager.GetCurrentScenario().LoseScenario((Weapons)cause);
    }

    private void OnBabyDoused(int cause)
    {
        Debug.Log("YOU DOUSED THE BABY!");
        Debug.Log("CURRENT SCENARIO: " + ScenarioManager.GetCurrentScenario());
        ScenarioManager.GetCurrentScenario().WinScenario();
    }
}
