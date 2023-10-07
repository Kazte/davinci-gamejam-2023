using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpNoShadow : MonoBehaviour, IPowerUp
{
    public float StartTime = 4f;

    private float currentTime = 0f;

    private bool isTimeRunning = false;
    private GameObject modifingCharacter;
    public void ActivatePowerUp(GameObject character)
    {
        if (modifingCharacter == null)
        {
            modifingCharacter = character;
        }
        //character.GetComponent<SHADOW>.SetShadow(false);
        isTimeRunning = true;
        currentTime = StartTime;
            

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
            HUDManager.Instance.SetPowerUpBlue(currentTime, StartTime);
        }
        else
        {
            if (isTimeRunning)
            {
                DeactivatePowerUp(modifingCharacter);
                HUDManager.Instance.SetPowerUpBlue(0, StartTime);
            }
        }
    }
}
