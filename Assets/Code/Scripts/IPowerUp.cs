using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp
{
    public void ActivatePowerUp(ShootingController Character);
    public void DeactivatePowerUp(GameObject Character);

}
