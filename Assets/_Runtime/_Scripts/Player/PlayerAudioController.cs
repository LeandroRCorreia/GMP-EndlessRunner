using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip rollSound;
    [SerializeField] private AudioClip jumpSound;

    private AudioSource audioSource; 
    public AudioSource AudioSource => audioSource == null ? this.GetComponent<AudioSource>() : audioSource;

    public void PlayRollSound()
    {
        Play(rollSound);
    }

    public void PlayJumpSound()
    {
        Play(jumpSound);
    }

    private void Play(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.loop = false;
        AudioSource.Play();
    }




}
