using System;
using System.Collections.Generic;

using UnityEngine;

using Random = UnityEngine.Random;

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

    public static Narration GetWinNarration()
    {
        Narration[] winSounds = { Narration.Win1, Narration.Win2, Narration.Win3, Narration.Win4, Narration.Win5 };
        return winSounds[Random.Range(0, winSounds.Length)];
    }

    public static Narration GetFailNarration(Weapons weapon)
    {
        switch (weapon)
        {
            case Weapons.Bat:
                return Narration.BatFail;
            case Weapons.Bottle:
                return Narration.BottleFail;
            case Weapons.BoxingGloves:
                return Narration.GlovesFail;
            case Weapons.Chainsaw:
                return Narration.SawFail;
            case Weapons.Gun:
                return Narration.GunFail;
            case Weapons.Knife:
                return Narration.KnifeFail;
            case Weapons.Sword:
                return Narration.SwordFail;
            case Weapons.FlameThrower:
                return Narration.FlameThrowerFail;
            default:
                return Narration.None;
        }
    }

    public static void PlayNarration(AudioSource audioSource, Narration narration)
    {
        if (narration == Narration.None)
        {
            return;
        }

        if (!NarrationMap.ContainsKey(narration))
        {
            NarrationMap.Add(narration, LoadAudioClip(narration));
        }
        AudioClip sound = NarrationMap[narration];
        if (sound != null)
        {
            // If we have an audio source, use it, otherwise play at the camera position
            if (audioSource != null)
            {
                audioSource.clip = sound;
                audioSource.Play();
            }
            else
            {
                AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
            }
        }
    }

    private static AudioClip LoadAudioClip(Narration narration)
    {
        return Resources.Load(narration.ToString()) as AudioClip;
    }

}
