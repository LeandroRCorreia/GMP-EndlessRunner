using UnityEngine;

public struct PowerUpMultiplierInfo
{

    public PowerUpMultiplierInfo(float multiplierPowerUp, float secondsPowerUp)
    {
        this.multiplierPowerUp = multiplierPowerUp;
        this.secondsPowerUp = secondsPowerUp;
    }

    public float multiplierPowerUp;
    public float secondsPowerUp;
}

public class PowerUpMultiplier : Pickup
{
    [SerializeField] private float multiplierPowerUp = 2f;
    [SerializeField] private float secondsPowerUp = 30f;

    protected override void ExecutePickupBehaviour(in PlayerCollisionInfo playerCollisionInfo)
    {
        var powerUpInfo = new PowerUpMultiplierInfo(multiplierPowerUp, secondsPowerUp);
        playerCollisionInfo.gameMode.OnPowerUpMultiplier(in powerUpInfo);

    }

}
