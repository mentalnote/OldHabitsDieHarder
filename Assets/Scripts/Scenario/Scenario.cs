using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//Delegate for flag triggers
public delegate void FlagTriggerDelegate(int cause);

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

    //Keep track of whether or not we are in the middle of processing triggers
    private bool currentlyProcessingTriggers = false;
    private Queue<KeyValuePair<int, FlagValue>> queuedFlagChanges;

    //Keep track of whether or not we have just won or lost, and are ignoring flag changes
    protected bool isScenarioOver = false;

    //Our flags
    protected Dictionary<int, FlagValue> flags;

    //Our flag triggers
    protected List<FlagTriggerDetails> flagTriggers;

    //This method needs to be called in the Awake() or Start() method of every concrete subclass
    protected void InitialiseScenario()
    {
        //Initialise the list of queued flag changes
        this.queuedFlagChanges = new Queue<KeyValuePair<int, FlagValue>>();

        //Initialise the list of flags
        this.flags = new Dictionary<int, FlagValue>();
        int[] flagKeys = EnumUtil.ArrayFromEnum(this.GetEnumType());
        foreach (int key in flagKeys)
        {
            this.flags[key] = new FlagValue(0, false);
        }

        //Initialise the list of flag triggers
        this.flagTriggers = new List<FlagTriggerDetails>();

        //When the scenario first becomes active, set it as the current scenario
        ScenarioManager.SetCurrentScenario(this);

        //If there is a flags debug instance present, set our enum type for it
        FlagsDebugDisplay flagDebug = this.gameObject.GetComponent<FlagsDebugDisplay>();
        if (flagDebug != null)
        {
            flagDebug.enumType = this.GetEnumType();
        }
    }

    //Sets the value of a flag
    public void SetFlag<K, T>(K causeVal, T flagKeyVal, bool value)
    {
        //If the scenario is already over and we're waiting for a level load, ignore all flag changes
        if (this.isScenarioOver == true)
        {
            return;
        }

        //Convert the cause to an int
        int cause = (int)System.Convert.ChangeType(causeVal, typeof(int));

        //Convert the flag key value to an int
        int flagKey = (int)System.Convert.ChangeType(flagKeyVal, typeof(int));

        //Sanity check
        if (!this.flags.ContainsKey((int)flagKey))
        {
            throw new System.IndexOutOfRangeException("Invalid flag: " + flagKey);
        }

        //If we're in the middle of processing flag change triggers, queue any changes
        if (this.currentlyProcessingTriggers == true)
        {
            this.queuedFlagChanges.Enqueue(new KeyValuePair<int, FlagValue>(flagKey, new FlagValue(cause, value)));
            return;
        }

        //Set the flag value and process any triggers
        this.flags[(int)flagKey] = new FlagValue(cause, value);
        this.ProcessTriggers();
    }

    //Retrieves the value of a flag
    public FlagValue GetFlag<T>(T flagKey)
    {
        return this.GetFlag((int)System.Convert.ChangeType(flagKey, typeof(int)));
    }

    //Retrieves the value of a flag
    public FlagValue GetFlag(int flagKey)
    {
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
            NarratorLibrary.PlayNarration(null, NarratorLibrary.GetWinNarration());

            //Show the "scenario won" text
            //...

            //Delay before the level load, so the player can see the transition
            Timer.SetTimer(7.0f, this.gameObject, delegate()
            {
                //Load the next level
                Application.LoadLevel(Application.loadedLevel + 1);
            });
        }
    }

    //"Loses" the scenario
    public void LoseScenario(Weapons loserWeapon)
    {
        //Prevent multiple successive calls to this method
        if (this.isScenarioOver == false)
        {
            //Set the "scenario over" flag
            this.isScenarioOver = true;

            //Play the narrator's sound clip
            NarratorLibrary.PlayNarration(null, NarratorLibrary.GetFailNarration(loserWeapon));

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
    public void RegisterFlagTrigger(int[] triggerFlagsSet, int[] triggerFlagsUnset, FlagTriggerDelegate handler, bool isOneShot = false)
    {
        this.flagTriggers.Add(new FlagTriggerDetails(triggerFlagsSet, triggerFlagsUnset, handler, isOneShot));
    }

    //Processes registered triggers whenever a flag changes
    private void ProcessTriggers()
    {
        //Ensure any calls to SetFlag() within our triggers are queued
        this.currentlyProcessingTriggers = true;

        //Maintain a list of triggered one-shot handlers to remove
        List<int> indicesToRemove = new List<int>();

        int triggerIndex = 0;
        foreach (FlagTriggerDetails trigger in this.flagTriggers)
        {
            int cause = 0;
            //Determine if the triggering flags that need to be set are set
            bool flagCriteriaMet = true;
            foreach (int flag in trigger.triggerFlagsSet)
            {
                flagCriteriaMet = flagCriteriaMet && this.GetFlag(flag).value;
                cause = this.GetFlag(flag).cause;
            }

            //Determine if the triggering flags that need to be unset are unset
            foreach (int flag in trigger.triggerFlagsUnset)
            {
                flagCriteriaMet = flagCriteriaMet && (this.GetFlag(flag).value == false);
            }

            //If all of the flags are set, call the handler
            if (flagCriteriaMet == true)
            {
                trigger.handler(cause);

                //If the handler was a one-shot, mark it for removal
                if (trigger.isOneShot)
                {
                    indicesToRemove.Add(triggerIndex);
                }
            }

            ++triggerIndex;
        }

        //Remove any triggers that were marked for removal
        foreach (int index in indicesToRemove)
        {
            this.flagTriggers.RemoveAt(index);
        }

        //Process any queued flag changes
        this.currentlyProcessingTriggers = false;
        while (this.queuedFlagChanges.Count > 0)
        {
            KeyValuePair<int, FlagValue> currPair = this.queuedFlagChanges.Dequeue();
            this.SetFlag(currPair.Value.cause, currPair.Key, true);
        }
    }

    //Concrete classes can override these methods if they want to:

    //Concrete classes must implement these methods:

    //Returns the enum type used for our flags
    abstract protected System.Type GetEnumType();

    public struct FlagValue
    {
        public int cause;
        public bool value;

        public FlagValue(int cause, bool value)
        {
            this.cause = cause;
            this.value = value;
        }
    }
}
