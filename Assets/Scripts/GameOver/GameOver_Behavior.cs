using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver_Behavior : MonoBehaviour
{
    public SoundData SDRef;

    private void Start()
    {
        SoundManager.PlaySound(SoundManager.SoundType.GameOver, SDRef.soundFiles);
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
