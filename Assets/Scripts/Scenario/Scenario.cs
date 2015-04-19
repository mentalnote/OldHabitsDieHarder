using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//Delegate for flag triggers
public delegate void FlagTriggerDelegate();

abstract public class Scenario : MonoBehaviour
{
	//Helper class for representing a flag trigger
	protected class FlagTriggerDetails
	{
		public FlagTriggerDetails(int[] triggerFlagsSet, int[] triggerFlagsUnset, FlagTriggerDelegate handler, bool isOneShot)
		{
			this.triggerFlagsSet = triggerFlagsSet;
			this.triggerFlagsUnset = triggerFlagsUnset;
			this.handler = handler;
			this.isOneShot = isOneShot;
		}

		public int[] triggerFlagsSet;
		public int[] triggerFlagsUnset;
		public FlagTriggerDelegate handler;
		public bool isOneShot;
	}

	//Keep track of whether or not we have just won or lost, and are ignoring flag changes
	protected bool isScenarioOver = false;

	//Our flags
	protected Dictionary<int, bool> flags;

	//Our flag triggers
	protected List<FlagTriggerDetails> flagTriggers;

	//This method needs to be called in the Awake() or Start() method of every concrete subclass
	protected void InitialiseScenario()
	{
		//Initialise the list of flags
		this.flags = new Dictionary<int, bool>();
		int[] flagKeys = this.GetFlagKeys();
		foreach (int key in flagKeys) {
			this.flags[key] = false;
		}

		//Initialise the list of flag triggers
		this.flagTriggers = new List<FlagTriggerDetails>();

		//When the scenario first becomes active, set it as the current scenario
		ScenarioManager.SetCurrentScenario(this);
	}

	//Sets the value of a flag
	public void SetFlag<T>(T flagKeyVal, bool value = true)
	{
		//If the scenario is already over and we're waiting for a level load, ignore all flag changes
		if (this.isScenarioOver == true) {
			return;
		}

		//Convert the flag key value to an int
		int flagKey = (int)System.Convert.ChangeType(flagKeyVal, typeof(int));

		//Sanity check
		if (!this.flags.ContainsKey((int)flagKey)) {
			throw new System.IndexOutOfRangeException("Invalid flag: " + flagKey);
		}

		//Set the flag value and process any triggers
		this.flags[(int)flagKey] = value;
		this.ProcessTriggers();
	}

	//Retrieves the value of a flag
	public bool GetFlag<T>(T flagKey) {
		return this.GetFlag( (int)System.Convert.ChangeType(flagKey, typeof(int)) );
	}

	//Retrieves the value of a flag
	public bool GetFlag(int flagKey) {
		return this.flags[flagKey];
	}

	//"Wins" the scenario
	public void WinScenario()
	{
		//Prevent multiple successive calls to this method
		if (this.isScenarioOver == false)
		{
			//Set the "scenario over" flag
			this.isScenarioOver = true;

			//Play the narrator's sound clip
			NarratorLibrary.PlayNarration (null, this.getScenarioWonNarration ());
		
			//Show the "scenario won" text
			//...
		
			//Delay before the level load, so the player can see the transition
			Timer.SetTimer(5.0f, this.gameObject, delegate()
			{
				//Load the next level
				Application.LoadLevel(Application.loadedLevel + 1);
			});
		}
	}

	//"Loses" the scenario
	public void LoseScenario()
	{
		//Prevent multiple successive calls to this method
		if (this.isScenarioOver == false)
		{
			//Set the "scenario over" flag
			this.isScenarioOver = true;

			//Play the narrator's sound clip
			NarratorLibrary.PlayNarration (null, this.getScenarioLostNarration ());

			//Show the "scenario lost" text
			//...

			//Delay before the level load, so the player can see the transition
			Timer.SetTimer(5.0f, this.gameObject, delegate()
			{
				//Restart the level
				Application.LoadLevel(Application.loadedLevel);
			});
		}
	}

	//Registers a flag trigger
	protected void RegisterFlagTrigger(int[] triggerFlagsSet, int[] triggerFlagsUnset, FlagTriggerDelegate handler, bool isOneShot = false) {
		this.flagTriggers.Add( new FlagTriggerDetails(triggerFlagsSet, triggerFlagsUnset, handler, isOneShot) );
	}
		
	//Processes registered triggers whenever a flag changes
	private void ProcessTriggers()
	{
		//Maintain a list of triggered one-shot handlers to remove
		List<int> indicesToRemove = new List<int>();

		int triggerIndex = 0;
		foreach (FlagTriggerDetails trigger in this.flagTriggers)
		{
			//Determine if the triggering flags that need to be set are set
			bool flagCriteriaMet = true;
			foreach (int flag in trigger.triggerFlagsSet) {
				flagCriteriaMet = flagCriteriaMet && this.GetFlag(flag);
			}

			//Determine if the triggering flags that need to be unset are unset
			foreach (int flag in trigger.triggerFlagsUnset) {
				flagCriteriaMet = flagCriteriaMet && (this.GetFlag(flag) == false);
			}

			//If all of the flags are set, call the handler
			if (flagCriteriaMet == true)
			{
				trigger.handler();

				//If the handler was a one-shot, mark it for removal
				if (trigger.isOneShot) {
					indicesToRemove.Add(triggerIndex);
				}
			}

			++triggerIndex;
		}

		//Remove any triggers that were marked for removal
		foreach (int index in indicesToRemove) {
			this.flagTriggers.RemoveAt(index);
		}
	}

	//Concrete classes can override these methods if they want to:
		
		//Returns the narration clip for when we win the scenario
		virtual protected Narration getScenarioWonNarration()  { return Narration.None; }

		//Returns the narration clip for when we lose the scenario
		virtual protected Narration getScenarioLostNarration() { return Narration.None; }
		
	//Concrete classes must implement these methods:
		
		//Returns the list of flag keys (the underlying int values of the enum)
		abstract protected int[] GetFlagKeys();
}
