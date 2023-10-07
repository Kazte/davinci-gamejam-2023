﻿using System;
using UnityEngine;
using Utilities;

public class GameManager : Singleton<GameManager>
{
    [Header("Garbage")]
    public int MaxGarbage = 20;

    private int currentGarbage;

    [Header("Enemies")]
    public int MaxEnemies = 30;

    private int currentEnemies;

    [Header("Game over")]
    public GameObject GameOverContainer;

    private float timer;

    private void Start()
    {
        timer = 0f;

        currentGarbage = 0;
        HUDManager.Instance.SetGarbageSlider(currentGarbage / (float)MaxGarbage);

        currentEnemies = MaxEnemies;
        HUDManager.Instance.SetEnemiesLeft(currentEnemies, MaxEnemies);

        GameOverContainer.SetActive(false);
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

        if (currentGarbage >= MaxGarbage)
        {
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
        HUDManager.Instance.SetEnemiesLeft(currentEnemies, MaxEnemies);
    }

    public void RemoveEnemy()
    {
        currentEnemies--;
        HUDManager.Instance.SetEnemiesLeft(currentEnemies, MaxEnemies);
    }

    public void GameOver()
    {
        GameOverContainer.SetActive(true);
    }
}