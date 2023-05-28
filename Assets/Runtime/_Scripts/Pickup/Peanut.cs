using UnityEngine;

public class Peanut : Pickup
{

    protected override void ExecutePickupBehaviour(in PlayerCollisionInfo playerCollisionInfo)
    {
        playerCollisionInfo.gameMode.PeanutsPicked++;
    }

}
