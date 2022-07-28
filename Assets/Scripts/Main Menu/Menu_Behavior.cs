using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Behavior : MonoBehaviour
{
    public SoundData soundData;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlayMusic(SoundManager.MusicType.MainMenu, soundData.musicFiles);
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
