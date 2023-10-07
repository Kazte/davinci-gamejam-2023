using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IPowerUp>(out var powerUp))
        {
            powerUp.ActivatePowerUp(this.GetComponent<ShootingController>());
        }
    
    }
}
