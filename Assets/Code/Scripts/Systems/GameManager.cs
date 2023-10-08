using System;
using UnityEngine;
using Utilities;

public class GameManager : Singleton<GameManager>
{
    [Header("Garbage")]
    public int MaxGarbage = 20;

    private int currentGarbage;

    [Header("Enemies")]
    public int MaxEnemies;

    private int currentEnemies;

    [Header("Game over")]
    public GameObject GameOverContainer;

    [Header("Win")]
    public GameObject WinContainer;

    private float timer;

    private bool blackPowerUp;
    private bool bluePowerUp;

    private void Start()
    {
        timer = 0f;

        currentGarbage = 0;
        HUDManager.Instance.SetGarbageSlider(currentGarbage / (float)MaxGarbage);

        currentEnemies = MaxEnemies;
        HUDManager.Instance.SetEnemiesLeft(currentEnemies, MaxEnemies);

        GameOverContainer.SetActive(false);
        WinContainer.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        HUDManager.Instance.SetTimer(timer);
    }

    public void AddGarbage()
    {
        currentGarbage++;

        HUDManager.Instance.SetGarbageSlider(currentGarbage / (float)MaxGarbage);
        if (currentGarbage>=MaxGarbage-(MaxGarbage*20/100))
        {
            AudioManager.Instance.Play("Alert_Loser");
        }
        if (currentGarbage >= MaxGarbage)
        {
            AudioManager.Instance.Play("Loser");
            GameOverContainer.SetActive(true);
        }
    }

    public void RemoveGarbage()
    {
        currentGarbage--;

        HUDManager.Instance.SetGarbageSlider(currentGarbage / (float)MaxGarbage);

        if (currentGarbage <= 0)
        {
            currentGarbage = 0;
        }
    }

    public void AddEnemy()
    {
        currentEnemies++;
        MaxEnemies++;
        HUDManager.Instance.SetEnemiesLeft(currentEnemies, MaxEnemies);
    }

    public void RemoveEnemy()
    {
        currentEnemies--;
        HUDManager.Instance.SetEnemiesLeft(currentEnemies, MaxEnemies);

        if (currentEnemies <= 0)
        {
            AudioManager.Instance.Play("Winner");
            WinContainer.SetActive(true);
        }
    }

    public void GameOver()
    {
        GameOverContainer.SetActive(true);
    }

    public void SetBlackPowerUp(bool set)
    {
        blackPowerUp = set;
    }

    public bool GetBlackPowerUp() => blackPowerUp;

    public void SetBluePowerUp(bool set)
    {
        bluePowerUp = set;
    }

    public bool GetBluePowerUp() => bluePowerUp;

    public int GetEnemiesLeft() => currentEnemies;

    public float GetScore() => timer;
}