using UnityEngine;
using System.Collections;

public class ExampleScenario : Scenario
{

    //Our flags enum
    public enum Flags
    {
        FlagOne,
        FlagTwo,
        FlagThree,
        FlagFour,
        FlagFive
    };

    //We need to implement this to tell the base class about our flag enum
    override protected System.Type GetEnumType()
    {
        return typeof(ExampleScenario.Flags);
    }

    void Start()
    {
        //We need to call InitialiseScenario() in either Awake() or Start()
        this.InitialiseScenario();

        //Register our flag triggers
        this.RegisterFlagTrigger(
            EnumUtil.ValuesToArray(new[] { Flags.FlagOne, Flags.FlagTwo }),
            new int[] { },
            new FlagTriggerDelegate(this.OnFirstTwoFlagsSet)
        );
        this.RegisterFlagTrigger(
            EnumUtil.ValuesToArray(new[] { Flags.FlagOne, Flags.FlagFive }),
            new int[] { },
            new FlagTriggerDelegate(this.OnFirstAndLastFlagsAreSet),
            true  //isOneShot
        );
        this.RegisterFlagTrigger(
            EnumUtil.ValuesToArray(new[] { Flags.FlagTwo }),
            EnumUtil.ValuesToArray(new[] { Flags.FlagFour }),
            new FlagTriggerDelegate(this.OnSecondFlagIsSetButFourthFlagIsNot)
        );
    }

    private void OnFirstTwoFlagsSet(int cause)
    {
        Debug.Log(System.DateTime.Now + " THE FIRST TWO FLAGS ARE SET!");
    }

    private void OnFirstAndLastFlagsAreSet(int cause)
    {
        Debug.Log("THE FIRST AND THE FIFTH FLAGS ARE SET!");
    }

    private void OnSecondFlagIsSetButFourthFlagIsNot(int cause)
    {
        Debug.Log("THE SECOND FLAG IS SET WHILE THE FOURTH IS NOT!");
    }
}
