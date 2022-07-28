using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{
    public enum SoundType
    {
        KingsDiceRoll,
        Buy,
        Sell,
        buildingPlacement,
        TaxesPaid
    }

    public enum MusicType
    {
        MainMenu,
        GameplayLoop,
        KingsPunishment
    }

    // For Music
    private static GameObject musicGameObject;
    private static AudioSource musicAudioSource;
    private static MusicContainer CurrentMusicPlaying { get; set; }
    
    // For SFX
    private static GameObject soundFXGameObject;
    private static AudioSource soundFXAudioSource;
    private static SoundFXContainer CurrentSoundPlaying { get; set; }

    //----------------------METHODS---------------------------------------------------------------------------

    // Make sure to give it the proper soundtype needed, and proper array for the clip wanted.
    public static void PlaySound(SoundType soundType, SoundFXContainer[] soundFiles)
    {
        if(soundFXGameObject == null)
        {
            soundFXGameObject = new GameObject("Sound");
            soundFXAudioSource = soundFXGameObject.AddComponent<AudioSource>();
            soundFXAudioSource.loop = false;
        }
        soundFXAudioSource.clip = GrabSoundClip(soundType, soundFiles);
        soundFXAudioSource.volume = CurrentSoundPlaying.audioVolume;

        soundFXAudioSource.PlayOneShot(CurrentSoundPlaying.audioClip);
    }

    // Make sure to give it the proper musictype needed, and proper array for the clip wanted.
    public static void PlayMusic(MusicType musicType, MusicContainer[] musicFiles)
    {
        if(musicGameObject == null)
        {
            musicGameObject = new GameObject("Music");
            musicAudioSource = musicGameObject.AddComponent<AudioSource>();
            musicAudioSource.loop = true;
            musicAudioSource.playOnAwake = false;
            
        }
        musicAudioSource.clip = GrabMusicClip(musicType, musicFiles);
        musicAudioSource.volume = CurrentMusicPlaying.audioVolume;

        musicAudioSource.Play();
    }

    private static AudioClip GrabSoundClip(SoundType soundType, SoundFXContainer[] soundFiles)
    {
        foreach (SoundFXContainer soundContainer in soundFiles)
        {
            if(soundContainer.soundType == soundType)
            {
                UpdateCurrentSoundFX(soundContainer);
                return soundContainer.audioClip;
            }
        }
        Debug.LogError("GrabSoundClip: it has returned null. has not found correct soundType");
        return null;
    }

    private static AudioClip GrabMusicClip(MusicType musicType, MusicContainer[] musicFiles)
    {
        foreach(MusicContainer musicContainer in musicFiles)
        {
            if(musicContainer.musicType == musicType)
            {
                UpdateCurrentMusic(musicContainer);
                return musicContainer.audioClip;
            }
        }
        Debug.LogError("GrabMusicClip: it has returned null. has not found correct musicType");
        return null;
    }

    private static void UpdateCurrentSoundFX(SoundFXContainer newCurrentSound)
    {
        CurrentSoundPlaying = newCurrentSound;
    }

    private static void UpdateCurrentMusic(MusicContainer newCurrentMusic)
    {
        CurrentMusicPlaying = newCurrentMusic;
    }
    
}
