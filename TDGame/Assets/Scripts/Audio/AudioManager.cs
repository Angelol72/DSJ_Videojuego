using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource uiAudioSource;
    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;

    [Header("Clips")]
    public AudioClip buttonClickClip;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayUIButtonClick()
    {
        if (buttonClickClip != null)
            uiAudioSource.PlayOneShot(buttonClickClip);
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
    }

    public void SetUIVolume(float volume)
    {
        uiAudioSource.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }
}