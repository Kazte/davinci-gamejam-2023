using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp
{
    public bool ActivatePowerUp(GameObject character);
    public void DeactivatePowerUp(GameObject character);

}
