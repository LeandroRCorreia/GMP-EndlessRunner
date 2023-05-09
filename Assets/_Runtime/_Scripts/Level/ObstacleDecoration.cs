using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class ObstacleDecoration : MonoBehaviour
{
    [SerializeField] private AudioClip collisionAudio;
    [SerializeField] private Animation collisionAnimation;

    private AudioSource audioSource;
    public AudioSource AudioSource => audioSource == null ? this.GetComponent<AudioSource>() : audioSource;

    private void Awake() 
    {
        collisionAnimation.playAutomatically = false;
    }

    public void PlayCollisionFeedBack()
    {
        AudioUtility.PlayAudioCue(AudioSource, collisionAudio);
        if(collisionAnimation != null)
        {
            collisionAnimation.Play();
        }
    }

}
