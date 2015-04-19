using UnityEngine;
using System.Collections;

public class FlagsDebugDisplay : MonoBehaviour
{
	//This will be set programmatically by the scenario
	public System.Type enumType = null;
	
	void OnGUI()
	{
		//Don't draw anything if the enum type has not been specified
		if (this.enumType == null) {
			return;
		}

		//Retrieve the list of enum values as ints
		int[] enumValues = EnumUtil.ArrayFromEnum(this.enumType);

		//Build the output text
		string debugText = "";
		foreach (int val in enumValues)
		{
			//Retrieve the enum value's name
			string valName = System.Enum.Parse(this.enumType, val.ToString()).ToString();

			//Determine whether or not the flag is set
			bool flagSet = ScenarioManager.GetCurrentScenario().GetFlag(val);

			//Append the line to the output
			debugText += valName + ": " + (flagSet ? "Set" : "Unset") + "\n";
		}

		GUI.color = Color.black;
		GUI.backgroundColor = new Color(0, 0, 0, 0);
		GUI.TextArea(new Rect (0, 0, 999999, 999999), debugText);
	}
}
