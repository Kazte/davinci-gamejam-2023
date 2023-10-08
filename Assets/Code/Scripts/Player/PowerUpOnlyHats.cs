using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpOnlyHats : MonoBehaviour, IPowerUp
{
    public float StartTime = 4f;

    private float currentTime = 0f;

    private bool isTimeRunning = false;
    private GameObject modifingCharacter;

    public float Cooldown = 10f;
    private float currentCooldown;
    private bool canTake;

    public SpriteRenderer SpriteRenderer;
    
    public bool ActivatePowerUp(GameObject character)
    {
        if (GameManager.Instance.GetBlackPowerUp()|| !canTake)
        {
            return false;
        }

        if (modifingCharacter == null)
        {
            modifingCharacter = character;
        }

        //character.GetComponent<SHADOW>.SetShadow(false);
        isTimeRunning = true;
        currentTime = StartTime;
        currentCooldown = Cooldown;
        return true;
    }

    public void DeactivatePowerUp(GameObject character)
    {
        //character.GetComponent<SHADOW>.SetShadow(true);
        isTimeRunning = true;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            //Actualizar Hud
            HUDManager.Instance.SetPowerUpBlack(currentTime, StartTime);
            GameManager.Instance.SetBlackPowerUp(true);
        }
        else
        {
            if (isTimeRunning)
            {
                DeactivatePowerUp(modifingCharacter);
                HUDManager.Instance.SetPowerUpBlack(0, StartTime);
                GameManager.Instance.SetBlackPowerUp(false);
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