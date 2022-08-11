using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{
    public SoundData SDRef;

    private void Start()
    {
        // there is no win screen sound or music file for now.
        // SoundManager.PlaySound(SoundManager.SoundType.GameOver, SDRef.soundFiles);
    }

    public void RestartTheGame()
    {
        SoundManager.PlaySound(SoundManager.SoundType.MenuButtonPress, SDRef.soundFiles);
        SceneManager.LoadScene(SceneChange.SampleScene.ToString());
    }

    public void ReturnToMainScreen()
    {
        SoundManager.PlaySound(SoundManager.SoundType.MenuButtonPress, SDRef.soundFiles);
        SceneManager.LoadScene(SceneChange.MainMenu.ToString());
    }
}
