using UnityEngine;

public class UiAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip buttonPressSound;
    [SerializeField] private AudioClip countDownSound;
    [SerializeField] private AudioClip countDownEnd;
    private AudioSource audioSource; 
    public AudioSource AudioSource => audioSource == null ? this.GetComponent<AudioSource>() : audioSource;

    public void PlayButtonPressSound()
    {
        Play(buttonPressSound);
    }

    public void PlayCountDownSound()
    {
        Play(countDownSound);
    }

    public void PlayCountDownEnd()
    {
        Play(countDownEnd);
    }

    private void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }


    private void Stop()
    {
        AudioSource.Stop();
    }

}
