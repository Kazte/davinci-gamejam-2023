using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp
{
    public void ActivatePowerUp(ShootingController character);
    public void DeactivatePowerUp(ShootingController character);

}
