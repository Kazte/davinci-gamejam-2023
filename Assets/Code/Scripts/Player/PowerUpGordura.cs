using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGordura : MonoBehaviour, IPowerUp
{
    public float StartTime = 5f;

    private float powerCoolDown;
    private float currentTime = 0f;

    private bool isTimeRunning = false;
    private GameObject modifingCharacter;

   
    public void ActivatePowerUp(GameObject character)
    {
        if (modifingCharacter == null)
        {
            modifingCharacter = character;
        }

        character.GetComponent<ShootingController>().SetGordura(true);
        isTimeRunning = true;
        currentTime = StartTime;
    } 

    public void DeactivatePowerUp(GameObject character)
    {
        character.GetComponent<ShootingController>().SetGordura(false);
        isTimeRunning = false;
        currentTime = 0;
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            // Actualizar HUD
            HUDManager.Instance.SetPowerUpGreen(currentTime, StartTime);
        }
        else
        {
            if (isTimeRunning)
            {
                DeactivatePowerUp(modifingCharacter);
                HUDManager.Instance.SetPowerUpGreen(0, StartTime);
            }
        }
    }
}