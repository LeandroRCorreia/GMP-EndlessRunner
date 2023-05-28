using UnityEngine;

public class PowerUpBuffInvincible : PowerUpBuff
{

    protected override void StartBehaviour()
    {
        playerControl.IsInvicible = true;

    }

    protected override void UpdateBehaviour()
    {
    }

    protected override void EndBehaviour()
    {
        playerControl.IsInvicible = false;

    }

}
