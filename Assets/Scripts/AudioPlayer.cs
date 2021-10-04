using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip deathSound;

    public void playAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(deathSound);
    }

}
