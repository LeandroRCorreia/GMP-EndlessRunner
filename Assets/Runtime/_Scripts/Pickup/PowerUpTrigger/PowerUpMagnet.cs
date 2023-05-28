using UnityEngine;

public class PowerUpMagnet : PowerUpTrigger
{

    protected override void ExecutePowerUpTrigger(in PlayerCollisionInfo playerCollisionInfo)
    {
        playerCollisionInfo.powerUpBuffControl.ActivePowerUpMagnet();
    }

}
