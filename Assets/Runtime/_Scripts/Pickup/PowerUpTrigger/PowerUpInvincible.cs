using UnityEngine;

public class PowerUpInvincible : PowerUpTrigger
{

    protected override void ExecutePowerUpTrigger(in PlayerCollisionInfo playerCollisionInfo)
    {
        playerCollisionInfo.powerUpBuffControl.ActivePowerUpInvincible();       

    }

}
