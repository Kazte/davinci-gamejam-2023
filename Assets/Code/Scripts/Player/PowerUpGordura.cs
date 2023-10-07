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

    // Start is called before the first frame update
    /*void Start()
    {
        switch (PowerTypeName)
        {
            case 0:
                powerCoolDown = 5f;

            case 1:
                powerCoolDown = 4f;

            case 2:
                powerCoolDown = 6f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeRunning == 1)
        {
            currentTime += currentTime.deltaTime;
            if (currentTime >= powerCoolDown)
            {
                currentTime = powerCoolDown;
                isTimeRunning = false;
                FirstPowerDeactivate();
            }
        }
    }

    public PowerType GetPowerType()
    {

        return PowerTypeName;
    }

    public void ActivatePower(gameObject character)
    {
        switch (PowerTypeName)
        {
            case 0:
                FirstPowerActivate();
                break;
            case 1:
                SecondPowerActivate();
                break;

            case 2:
                ThirdPowerActivate();
                break;
        }
        Debug.Log("PowerActivate " );
    }
    void FirstPowerActivate()
    {
        ShottingController.currentAmmo = 500;
        initTime = Time.time;
        isTimeRunning = true;
    }
    void SecondPowerActivate()
    {
    }
    void ThirdPowerActivate()
    {

    }

    void FirstPowerDeactivate()
    {
        ShottingController.CurrentAmmo = 5;
    }*/
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