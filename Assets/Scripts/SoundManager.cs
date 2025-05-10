using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    public AudioClip buttonClickSound2;

    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }


    public void PlayAgainSounds()
    {
        audioSource.PlayOneShot(buttonClickSound2);
    }
}
