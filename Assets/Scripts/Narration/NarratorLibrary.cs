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
		if (narration == Narration.None) {
			return;
		}

        if (!NarrationMap.ContainsKey(narration))
        {
            NarrationMap.Add(narration, LoadAudioClip(narration));
        }
        AudioClip sound = NarrationMap[narration];

		//If we have an audio source, use it, otherwise play at the camera position
		if (audioSource != null)
		{
			audioSource.clip = sound;
			audioSource.Play ();

		} else {
			AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
		}
    }

    private static AudioClip LoadAudioClip(Narration narration)
    {
        return Resources.Load(narration.ToString()) as AudioClip;
    }

}
