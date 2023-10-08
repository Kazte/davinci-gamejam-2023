using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp
{
    public void ActivatePowerUp(GameObject character);
    public void DeactivatePowerUp(GameObject character);

}
