using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBatman : MonoBehaviour, IPowerUp
{
    public float StartTime = 6f;

    private float currentTime = 0f;

    private bool isTimeRunning = false;
    private GameObject modifingCharacter;
    private Color lastColor;

    public float Cooldown = 10f;
    private float currentCooldown;
    private bool canTake;

    public SpriteRenderer SpriteRenderer;

    public bool ActivatePowerUp(GameObject character)
    {
        if (GameManager.Instance.GetBluePowerUp() || !canTake)
        {
            return false;
        }

        //character.GetComponent.LIGHTOUT
        if (modifingCharacter == null)
        {
            modifingCharacter = GameObject.Find("Directional Light");
        }

        modifingCharacter.GetComponent<Light>().color = Color.grey;

        isTimeRunning = true;
        currentTime = StartTime;

        currentCooldown = Cooldown;

        return true;
    }

    public void DeactivatePowerUp(GameObject character)
    {
        modifingCharacter.GetComponent<Light>().color = new Color(1f, 0.9568f, 0.8392f);
        isTimeRunning = false;
        currentTime = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            //Actualizar Hud
            HUDManager.Instance.SetPowerUpBlue(currentTime, StartTime);
            GameManager.Instance.SetBluePowerUp(true);
        }
        else
        {
            if (isTimeRunning)
            {
                DeactivatePowerUp(modifingCharacter);
                HUDManager.Instance.SetPowerUpBlue(0, StartTime);
                GameManager.Instance.SetBluePowerUp(false);
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