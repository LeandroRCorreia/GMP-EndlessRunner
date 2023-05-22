using UnityEngine;

public class Cherries : Pickup
{

    protected override void ExecutePickupBehaviour(in PlayerCollisionInfo playerCollisionInfo)
    {
        playerCollisionInfo.gameMode.CherriesPicked++;
    }

}
