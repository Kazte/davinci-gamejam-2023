using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ShootingController shootingController;


    private void Awake()
    {
        shootingController = GetComponent<ShootingController>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IPowerUp>(out var powerUp))
        {
            if (powerUp.ActivatePowerUp(this.gameObject))
            {
                AudioManager.Instance.Play("Take_PowerUp");
                this.gameObject.GetComponentInChildren<Animator>().SetTrigger("PowerUp");
            }
        }
    }
}