using UnityEngine;
using System.Collections;

public class PlayWeaponSound : MonoBehaviour
{


    [SerializeField]
    private AudioSource audioSource;

    public void Play()
    {
        this.audioSource.Play();
    }

    public void Stop()
    {
        this.audioSource.Stop();
    }

}
