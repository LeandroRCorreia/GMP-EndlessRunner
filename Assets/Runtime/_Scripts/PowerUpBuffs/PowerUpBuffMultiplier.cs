using UnityEngine;

public class PowerUpBuffMultiplier : PowerUpBuff
{
    public int scoreMultiplierBuff = 2;

    protected override void StartBehaviour()
    {
        gameMode.TemporaryScoreMultiplier = scoreMultiplierBuff;
    }

    protected override void UpdateBehaviour()
    {
        
    }

    protected override void EndBehaviour()
    {
        gameMode.TemporaryScoreMultiplier = 0;
        
    }

}
