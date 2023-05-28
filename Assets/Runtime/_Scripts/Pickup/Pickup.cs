using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(CollectedInRun))]
public abstract class Pickup : MonoBehaviour, IPlayerCollisionReact
{
    private CollectedInRun collectedInRun;
    [SerializeField] private float rotateSpeed = 100f;
    private CollectedInRun Collected =>
    collectedInRun != null ? collectedInRun : GetComponent<CollectedInRun>();
    protected abstract void ExecutePickupBehaviour(in PlayerCollisionInfo playerCollisionInfo);

    private void LateUpdate() 
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.Self);
    }

    public void ReactPlayerCollision(in PlayerCollisionInfo playerCollisionInfo)
    {
        if(Collected.IsCollected) return;
        ExecutePickupBehaviour(playerCollisionInfo);
        Collected.ExecuteCollected();
        
    }




    
}
