using UnityEngine;

public class PowerUpMultiplier : PowerUpTrigger
{
    protected override void ExecutePowerUpTrigger(in PlayerCollisionInfo playerCollisionInfo)
    {
        playerCollisionInfo.powerUpBuffControl.ActivePowerUpMultiplier();
            
    }

}
