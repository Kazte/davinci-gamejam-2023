using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;

public class HUDManager : Singleton<HUDManager>
{
    [Header("Top Container")]
    [Space] public Slider GarbageSlider;

    public GameObject GarbageSliderImage;

    [Space] public TMP_Text TimerText;

    [Space] public Image EnemiesLeftSlider;

    public TMP_Text EnemiesLeftText;


    [Header("Bottom Left Container")]
    [Space] public List<AmmoUI> AmmoUis = new List<AmmoUI>();


    [Header("Power Up")]
    [Space] public Image GreenPowerUpSlider;

    public TMP_Text GreenPowerUpText;
    public GameObject GreenPowerUpContainer;

    [Space] public Image BlackPowerUpSlider;

    public TMP_Text BlackPowerUpText;
    public GameObject BlackPowerUpContainer;

    [Space] public Image BluePowerUpSlider;

    public TMP_Text BluePowerUpText;
    public GameObject BluePowerUpContainer;

    private void OnDisable()
    {
        GarbageSliderImage.transform.DOKill();
    }

    private void Start()
    {
        SetPowerUpGreen(0, 0);
        SetPowerUpBlack(0, 0);
        SetPowerUpBlue(0, 0);
    }


    private bool garbageImageTweening;

    public void SetGarbageSlider(float percent)
    {
        GarbageSlider.value = percent;

        if (percent > 0.5f)
        {
            if (!garbageImageTweening)
            {
                if (GarbageSliderImage.transform != null)
                    GarbageSliderImage.transform.DOScale(Vector3.one * 1.3f, 0.2f).SetLoops(-1, LoopType.Yoyo);
                garbageImageTweening = true;
            }
        }
        else
        {
            GarbageSliderImage.transform.DORestart();
            GarbageSliderImage.transform.DOPause();
            garbageImageTweening = false;
        }
    }

    public void SetTimer(float durationInSeconds)
    {
        var seconds = Mathf.FloorToInt(durationInSeconds % 60);
        var minutes = Mathf.FloorToInt(durationInSeconds / 60);

        TimerText.SetText($"{minutes:00}:{seconds:00}");
    }

    public void SetEnemiesLeft(int enemiesLeft, int maxEnemies)
    {
        EnemiesLeftSlider.fillAmount = 1 - (enemiesLeft / (float)maxEnemies);
        EnemiesLeftText.SetText(enemiesLeft.ToString());
    }

    private bool fullAmmo = true;

    public void SetAmmo(int ammo)
    {
        for (var i = 0; i < AmmoUis.Count; i++)
        {
            var ammoUi = AmmoUis[i];

            if (i == ammo - 1 && (!fullAmmo || ammo < 6))
            {
                ammoUi.Fill();
                ammoUi.PlayFillEffect();
            }
            else if (i < ammo)
            {
                ammoUi.Fill();
            }
            else
            {
                ammoUi.Empty();
            }
        }

        fullAmmo = ammo switch
        {
            6 => true,
            < 6 => false,
            _ => fullAmmo
        };
    }

    public void SetPowerUpGreen(float timeLeft, float totalTime)
    {
        if (timeLeft > 0)
        {
            GreenPowerUpSlider.fillAmount = timeLeft / totalTime;
            GreenPowerUpText.SetText(timeLeft.ToString("0.00s"));
            GreenPowerUpContainer.gameObject.SetActive(true);
        }
        else
        {
            GreenPowerUpText.SetText(string.Empty);
            GreenPowerUpSlider.fillAmount = 0;
            GreenPowerUpContainer.gameObject.SetActive(false);
        }
    }


    public void SetPowerUpBlack(float timeLeft, float totalTime)
    {
        if (timeLeft > 0)
        {
            BlackPowerUpSlider.fillAmount = timeLeft / totalTime;
            BlackPowerUpText.SetText(timeLeft.ToString());
            BlackPowerUpContainer.gameObject.SetActive(true);
        }
        else
        {
            BlackPowerUpText.SetText(string.Empty);
            BlackPowerUpSlider.fillAmount = 0;
            BlackPowerUpContainer.gameObject.SetActive(false);
        }
    }

    public void SetPowerUpBlue(float timeLeft, float totalTime)
    {
        if (timeLeft > 0)
        {
            BluePowerUpSlider.fillAmount = timeLeft / totalTime;
            BluePowerUpText.SetText(timeLeft.ToString());
            BluePowerUpContainer.gameObject.SetActive(true);
        }
        else
        {
            BluePowerUpText.SetText(string.Empty);
            BluePowerUpSlider.fillAmount = 0;
            BluePowerUpContainer.gameObject.SetActive(false);
        }
    }
}