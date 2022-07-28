using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float initialTime;
    public float timeRemaining;
    public bool isTimerOn = false;
    public bool isTimerPaused = false;

    public TMP_Text textMeshProText;

    // Start is called before the first frame update
    void Start()
    {
        initialTime = timeRemaining;
        isTimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {
            if (!isTimerPaused)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    UpdateTimer(timeRemaining);
                }
                else
                {
                    timeRemaining = 0;
                    isTimerOn = false;
                }
            }
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        textMeshProText.text = string.Format("{0:00} : {1:00}",minutes,seconds);
    }

    public void RestartTimer()
    {
        timeRemaining = initialTime;
        isTimerOn = true;
    }

    public void PauseTimer()
    {
        isTimerPaused = true;
    }

    public void UnPauseTimer()
    {
        isTimerPaused = false;
    }
}
