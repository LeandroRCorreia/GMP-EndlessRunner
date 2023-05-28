using UnityEngine;

[RequireComponent(typeof(CollectedInRun))]
public abstract class PowerUpTrigger : MonoBehaviour, IPlayerCollisionReact
{
    protected CollectedInRun collectedInRun;
    private CollectedInRun Collected =>
    collectedInRun != null ? collectedInRun : GetComponent<CollectedInRun>();

    protected abstract void ExecutePowerUpTrigger(in PlayerCollisionInfo playerCollisionInfo);

    public void ReactPlayerCollision(in PlayerCollisionInfo playerCollisionInfo)
    {
        if(Collected.IsCollected) return;
        ExecutePowerUpTrigger(playerCollisionInfo);
        Collected.ExecuteCollected();
        
    }

}
