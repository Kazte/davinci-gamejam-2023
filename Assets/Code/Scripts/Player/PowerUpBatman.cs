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

    public bool ActivatePowerUp(GameObject character)
    {
        if (GameManager.Instance.GetBluePowerUp())
        {
            return false;
        }

        //character.GetComponent.LIGHTOUT
        if (modifingCharacter == null)
        {
            modifingCharacter = GameObject.Find("Directional Light");
        }

        lastColor = modifingCharacter.GetComponent<Light>().color;
        modifingCharacter.GetComponent<Light>().color = Color.grey;
        isTimeRunning = true;
        currentTime = StartTime;

        return true;
    }

    public void DeactivatePowerUp(GameObject character)
    {
        modifingCharacter.GetComponent<Light>().color = lastColor;
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
    }
}