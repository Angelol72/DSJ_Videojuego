using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("UI Sounds")]
    public AudioClip buttonClickSound;
    
    [Header("Audio Settings")]
    [Range(0f, 1f)]
    public float uiVolume = 0.7f;
    
    private AudioSource audioSource;
    
    void Awake()
    {
        // Singleton pattern - solo una instancia
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayButtonClick()
    {
        PlaySound(buttonClickSound);
    }
    
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, uiVolume);
        }
    }
}