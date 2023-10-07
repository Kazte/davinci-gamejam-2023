using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;

public class HUDManager : Singleton<HUDManager>
{
    [Header("Top Container")]
    [Space] public Image GarbageSlider;

    [Space] public TMP_Text TimerText;

    [Space] public Image EnemiesLeftSlider;

    public TMP_Text EnemiesLeftText;


    [Header("Bottom Left Container")]
    [Space] public List<AmmoUI> AmmoUis = new List<AmmoUI>();


    [Header("Power Up")]
    [Space] public Image GreenPowerUpSlider;

    public TMP_Text GreenPowerUpText;

    [Space] public Image BlackPowerUpSlider;

    public TMP_Text BlackPowerUpText;

    [Space] public Image BluePowerUpSlider;

    public TMP_Text BluePowerUpText;


    public void SetGarbageSlider(float percent)
    {
        GarbageSlider.fillAmount = percent;
    }

    public void SetTimer(float durationInSeconds)
    {
        var seconds = Mathf.FloorToInt(durationInSeconds % 60);
        var minutes = Mathf.FloorToInt(durationInSeconds / 60);

        TimerText.SetText($"{minutes:00}:{seconds:00}");
    }

    public void SetEnemiesLeft(int enemiesLeft, int maxEnemies)
    {
        EnemiesLeftSlider.fillAmount = enemiesLeft / (float)maxEnemies;
        EnemiesLeftText.SetText(enemiesLeft.ToString());
    }

    public void SetAmmo(int ammo)
    {
        for (var i = 0; i < AmmoUis.Count; i++)
        {
            var ammoUi = AmmoUis[i];

            if (i < ammo)
            {
                ammoUi.Fill();
            }
            else
            {
                ammoUi.Empty();
            }
        }
    }

    public void SetPowerUpGreen(float timeLeft, float totalTime)
    {
        if (timeLeft > 0)
        {
            GreenPowerUpSlider.fillAmount = timeLeft / totalTime;
            GreenPowerUpText.SetText(timeLeft.ToString("0.00s"));
        }
        else
        {
            GreenPowerUpText.SetText(string.Empty);
            GreenPowerUpSlider.fillAmount = 0;
        }
    }


    public void SetPowerUpBlack(float timeLeft, float totalTime)
    {
        if (timeLeft > 0)
        {
            BlackPowerUpSlider.fillAmount = 1f - timeLeft / totalTime;
            BlackPowerUpText.SetText(timeLeft.ToString());
        }
        else
        {
            BlackPowerUpText.SetText(string.Empty);
            BlackPowerUpSlider.fillAmount = 0;
        }
    }

    public void SetPowerUpBlue(float timeLeft, float totalTime)
    {
        if (timeLeft > 0)
        {
            BluePowerUpSlider.fillAmount = 1f - timeLeft / totalTime;
            BluePowerUpText.SetText(timeLeft.ToString());
        }
        else
        {
            BluePowerUpText.SetText(string.Empty);
            BluePowerUpSlider.fillAmount = 0;
        }
    }
}