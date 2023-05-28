using UnityEngine;

public static class AudioUtility
{

    public static void PlayAudioCue(AudioSource source, AudioClip clip)
    {
        if(source.outputAudioMixerGroup == null)
        {
            Debug.LogError("É obrigatório que um AudioSource contenha um MixerGroup!!");
        }
        else
        {
            source.clip = clip;
            source.loop = false;
            source.Play();
        }

    }

    public static void PlayMusic(AudioSource source, AudioClip clip)
    {
        if(source.outputAudioMixerGroup == null)
        {
            Debug.LogError("É obrigatório que um AudioSource contenha um MixerGroup!!");
        }
        else
        {
            source.clip = clip;
            source.loop = true;
            source.Play();
        }
    }

}
