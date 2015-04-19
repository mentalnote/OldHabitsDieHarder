using System;

using UnityEngine;

public class TestNarrator : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Narration narration;

    public void PlayNarration()
    {
        NarratorLibrary.PlayNarration(this.audioSource, this.narration);
    }

}
