using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
public abstract class Pickup : MonoBehaviour, IPlayerCollisionReact
{
    [SerializeField] private GameObject model;
    [SerializeField] private AudioClip pickupAudio;
    [SerializeField] private float rotateSpeed = 100f;

    private AudioSource audioSource;
    private BoxCollider boxCollider;
    protected bool isCollected = false;

    private BoxCollider BoxColl => boxCollider == null ? GetComponent<BoxCollider>() : boxCollider;
    private AudioSource AudioSource => audioSource == null ? GetComponent<AudioSource>() : audioSource;

    protected virtual float LifeAfterPickup => pickupAudio.length;

    protected abstract void ExecutePickupBehaviour(in PlayerCollisionInfo playerCollisionInfo);
    
    private void Start() 
    {
        BoxColl.isTrigger = true;    
        isCollected = false;
    }

    private void Update() 
    {
        if(isCollected) return;
        
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.Self);
    }

    public void ReactPlayerCollision(in PlayerCollisionInfo playerCollisionInfo)
    {
        OnPickedUp(in playerCollisionInfo);
    }

    private void OnPickedUp(in PlayerCollisionInfo playerCollisionInfo)
    {
        if(isCollected) return;
        isCollected = true;
        
        AudioUtility.PlayAudioCue(AudioSource, pickupAudio);
        if(model != null) model.SetActive(false);
        Destroy(gameObject, pickupAudio.length);
        ExecutePickupBehaviour(playerCollisionInfo);
    }

}
