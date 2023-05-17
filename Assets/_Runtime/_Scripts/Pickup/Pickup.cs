using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
public class Pickup : MonoBehaviour, IPlayerCollisionReact
{
    [SerializeField] private GameObject model;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private float rotateSpeed = 100f;

    private AudioSource audioSource;
    private BoxCollider boxCollider;
    private bool isCollected = false;
    private AudioSource AudioSource => audioSource == null ? GetComponent<AudioSource>() : audioSource;

    private void Awake() 
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start() 
    {
        boxCollider.isTrigger = true;    
        isCollected = false;
    }

    private void Update() 
    {
        if(isCollected) return;
        
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.Self);
    }

    public void ReactPlayerCollision(in PlayerCollisionInfo playerCollisionInfo)
    {
        playerCollisionInfo.gameMode.OnCherriesPickup();
        OnPickedUp();
    }

    public void OnPickedUp()
    {
        if(isCollected) return;
        isCollected = true;
        AudioUtility.PlayAudioCue(AudioSource, pickupSound);
        if(model != null) model.SetActive(false);
        Destroy(gameObject, pickupSound.length);

    }

}
