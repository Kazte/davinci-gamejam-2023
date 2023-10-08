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

    public float Cooldown = 10f;
    private float currentCooldown;
    private bool canTake;

    public SpriteRenderer SpriteRenderer;

    public bool ActivatePowerUp(GameObject character)
    {
        if (GameManager.Instance.GetGreenPowerUp() || !canTake)
        {
            return false;
        }

        if (modifingCharacter == null)
        {
            modifingCharacter = character;
        }

        character.GetComponent<ShootingController>().SetGordura(true);
        isTimeRunning = true;
        currentTime = StartTime;
        GameManager.Instance.SetGreenPowerUp(true);

        currentCooldown = Cooldown;

        return true;
    }

    public void DeactivatePowerUp(GameObject character)
    {
        character.GetComponent<ShootingController>().SetGordura(false);
        isTimeRunning = false;
        GameManager.Instance.SetGreenPowerUp(false);
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

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            canTake = false;

            SpriteRenderer.color = new Color(1, 1, 1, 0.25f);
        }
        else
        {
            currentCooldown = 0;
            canTake = true;
            SpriteRenderer.color = Color.white;
        }
    }
}