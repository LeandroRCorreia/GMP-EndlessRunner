using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
public class CollectedInRun : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private AudioClip pickupAudio;
    protected virtual float LifeAfterPickup => pickupAudio.length;

    private AudioSource audioSource;
    private BoxCollider boxCollider;
    
    private BoxCollider BoxColl => boxCollider == null ? GetComponent<BoxCollider>() : boxCollider;
    private AudioSource AudioSource => audioSource == null ? GetComponent<AudioSource>() : audioSource;

    public bool IsCollected {get; private set;}

    private void Start() 
    {        
        BoxColl.isTrigger = true;
        IsCollected = false;
    }

    public void ExecuteCollected()
    {
        if (IsCollected) return;
        IsCollected = true;
        AudioUtility.PlayAudioCue(AudioSource, pickupAudio);
        if (model != null) model.SetActive(false);
        Destroy(gameObject, LifeAfterPickup);
    }

}
