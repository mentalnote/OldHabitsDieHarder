using UnityEngine;
using System.Collections;

public class TestGUI : MonoBehaviour
{
	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 100, 100), "Set Flag 1")) {
			ScenarioManager.GetCurrentScenario().SetFlag( ExampleScenario.Flags.FlagOne );
		}

		if (GUI.Button(new Rect(100, 0, 100, 100), "Set Flag 2")) {
			ScenarioManager.GetCurrentScenario().SetFlag( ExampleScenario.Flags.FlagTwo );
		}

		if (GUI.Button(new Rect(200, 0, 100, 100), "Set Flag 3")) {
			ScenarioManager.GetCurrentScenario().SetFlag( ExampleScenario.Flags.FlagThree );
		}

		if (GUI.Button(new Rect(300, 0, 100, 100), "Set Flag 4")) {
			ScenarioManager.GetCurrentScenario().SetFlag( ExampleScenario.Flags.FlagFour );
		}

		if (GUI.Button(new Rect(400, 0, 100, 100), "Set Flag 5")) {
			ScenarioManager.GetCurrentScenario().SetFlag( ExampleScenario.Flags.FlagFive );
		}

		if (GUI.Button(new Rect(0, 100, 100, 100), "Unset Flag 1")) {
			ScenarioManager.GetCurrentScenario().SetFlag( ExampleScenario.Flags.FlagOne, false );
		}
		
		if (GUI.Button(new Rect(100, 100, 100, 100), "Unset Flag 2")) {
			ScenarioManager.GetCurrentScenario().SetFlag( ExampleScenario.Flags.FlagTwo, false );
		}
		
		if (GUI.Button(new Rect(200, 100, 100, 100), "Unset Flag 3")) {
			ScenarioManager.GetCurrentScenario().SetFlag( ExampleScenario.Flags.FlagThree, false );
		}
		
		if (GUI.Button(new Rect(300, 100, 100, 100), "Unset Flag 4")) {
			ScenarioManager.GetCurrentScenario().SetFlag( ExampleScenario.Flags.FlagFour, false );
		}
		
		if (GUI.Button(new Rect(400, 100, 100, 100), "Unset Flag 5")) {
			ScenarioManager.GetCurrentScenario().SetFlag( ExampleScenario.Flags.FlagFive, false );
		}

		string debugText = "";
		debugText += "Flag 1: " + ScenarioManager.GetCurrentScenario().GetFlag(ExampleScenario.Flags.FlagOne)   + "\n";
		debugText += "Flag 2: " + ScenarioManager.GetCurrentScenario().GetFlag(ExampleScenario.Flags.FlagTwo)   + "\n";
		debugText += "Flag 3: " + ScenarioManager.GetCurrentScenario().GetFlag(ExampleScenario.Flags.FlagThree) + "\n";
		debugText += "Flag 4: " + ScenarioManager.GetCurrentScenario().GetFlag(ExampleScenario.Flags.FlagFour)  + "\n";
		debugText += "Flag 5: " + ScenarioManager.GetCurrentScenario().GetFlag(ExampleScenario.Flags.FlagFive)  + "\n";
		GUI.TextArea(new Rect (0, 250, 999999, 200), debugText);
	}
}
