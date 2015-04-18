using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// The narrator's library of audio-clips. 
/// The narrators lines can be played from here.
/// </summary>
public class NarratorLibrary : MonoBehaviour
{

    /// <summary>
    /// A map of narrations to their respective audioclips.
    /// </summary>
    private static readonly Dictionary<Narration, AudioClip> NarrationMap = new Dictionary<Narration, AudioClip>();

    public static void PlayNarration(AudioSource audioSource, Narration narration)
    {
        if (!NarrationMap.ContainsKey(narration))
        {
            NarrationMap.Add(narration, LoadAudioClip(narration));
        }
        AudioClip sound = NarrationMap[narration];

        audioSource.clip = sound;
        audioSource.Play();
    }

    private static AudioClip LoadAudioClip(Narration narration)
    {
        return Resources.Load(narration.ToString()) as AudioClip;
    }

}
