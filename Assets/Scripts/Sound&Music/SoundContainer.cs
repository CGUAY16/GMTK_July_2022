using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioContainer
{
    public AudioClip audioClip;
    public float audioVolume;

    public AudioContainer()
    {}

    public float GetAudioClipLength()
    {
        return audioClip.length;
    }
    public float GetAudioClipVolume()
    {
        return audioVolume;
    }
}

[System.Serializable]
public class MusicContainer : AudioContainer
{
    public SoundManager.MusicType musicType;
}

[System.Serializable]
public class SoundFXContainer : AudioContainer 
{
    public SoundManager.SoundType soundType;
}  


