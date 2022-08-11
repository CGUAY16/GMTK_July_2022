using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum SceneChange 
{ 
    SampleScene,
    MainMenu,
    GameOver,
    WinScreen
}

public class Menu_Behavior : MonoBehaviour
{
    public SoundData soundData;
    [SerializeField] Button playB;
    [SerializeField] Button helpB;
    [SerializeField] Button quitB;

    [SerializeField] RectTransform helpScreenPanel;
    [SerializeField] RectTransform MainMenuPanel;

    [SerializeField] TMP_Text[] helpScreenTextObjects;
    private TMP_Text currentTextObjectViewed;
    private int textArrayIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlayMusic(SoundManager.MusicType.MainMenu, soundData.musicFiles);
        helpScreenPanel.gameObject.SetActive(false);
        MainMenuPanel.gameObject.SetActive(true);


    }

    public void Play()
    {
        SceneManager.LoadScene(SceneChange.SampleScene.ToString());
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HelpScreenActivate()
    {
        helpScreenPanel.gameObject.SetActive(true);
        MainMenuPanel.gameObject.SetActive(false);

        currentTextObjectViewed = helpScreenTextObjects[0];
        textArrayIndex = 0;

        foreach(TMP_Text text in helpScreenTextObjects)
        {
            if(text == currentTextObjectViewed)
            {
                text.gameObject.SetActive(true);
            }
            else
            {
                text.gameObject.SetActive(false);
            }
        }
        
    }

    public void ReturnToMainMenuSetup()
    {
        helpScreenPanel.gameObject.SetActive(false);
        MainMenuPanel.gameObject.SetActive(true);
    }

    public void NextPagePls()
    {
        textArrayIndex++;
        if(textArrayIndex >= helpScreenTextObjects.Length)
        {
            Debug.Log("this is outside the array");
            textArrayIndex--;
            return;
        }
        currentTextObjectViewed = helpScreenTextObjects[textArrayIndex];
        foreach (TMP_Text text in helpScreenTextObjects)
        {
            if (text == currentTextObjectViewed)
            {
                text.gameObject.SetActive(true);
            }
            else
            {
                text.gameObject.SetActive(false);
            }
        }
    }

    public void PrevPagePls()
    {
        textArrayIndex--;
        if (textArrayIndex < 0)
        {
            Debug.Log("this is outside the array");
            textArrayIndex++;
            return;
        }
        currentTextObjectViewed = helpScreenTextObjects[textArrayIndex];
        foreach (TMP_Text text in helpScreenTextObjects)
        {
            if (text == currentTextObjectViewed)
            {
                text.gameObject.SetActive(true);
            }
            else
            {
                text.gameObject.SetActive(false);
            }
        }
    }

}
