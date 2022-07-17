using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_SFX_manager : MonoBehaviour
{
    public AudioClip[] soundClips;
    public AudioClip[] trackClips;

    public AudioSource audio;

    private void Awake()
    {
        audio.volume = .25f;
        audio = GetComponent<AudioSource>();
    }

    public void PlayThisClip(AudioClip track)
    {
        audio.clip = track;
        audio.volume = .25f;
        audio.Play();
        audio.loop = true;
    }
}
