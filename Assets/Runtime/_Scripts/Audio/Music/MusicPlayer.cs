using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{

    [SerializeField] private AudioClip startMenuMusic; 
    [SerializeField] private AudioClip maintrackMusic;

    [SerializeField] private AudioClip deathMusic;

    private AudioSource audioSource; 
    public AudioSource AudioSource => audioSource == null ? this.GetComponent<AudioSource>() : audioSource;

    public void PlayMenuTheme()
    {
        PlayMusic(startMenuMusic);
    }

    public void PlayMainMusic()
    {
        PlayMusic(maintrackMusic);
    }

    public void PlayDeathMusic()
    {
        PlayMusic(deathMusic);
    }

    private void PlayMusic(AudioClip clip)
    {
        if(clip == AudioSource.clip) return;
        AudioUtility.PlayMusic(AudioSource, clip);
    }

    public void StopMusic()
    {
        AudioSource.Stop();
    }


}
