using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        UpdateTimer(0f);
    }

    public void UpdateTimer(float timeRemaining)
    {
        int minutes = (int)timeRemaining / 60;
        int seconds = (int)timeRemaining % 60;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
