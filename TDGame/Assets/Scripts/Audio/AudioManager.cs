using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Mixer")]
    public AudioMixer audioMixer;

    // Nombres de parámetros REALES en tu mixer
    private const string MASTER_PARAM = "mainVolume";
    private const string MUSIC_PARAM = "musicVolume";
    private const string SFX_PARAM = "fxVolume";

    // Keys para PlayerPrefs (pueden ser iguales o diferentes)
    private const string MASTER_KEY = "mainVolume";
    private const string MUSIC_KEY = "musicVolume";
    private const string SFX_KEY = "fxVolume";
    private const string MUTE_KEY = "Muted";

    private void Awake()
    {
        // Singleton persistente
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
    }

    public void SetVolume(string parameter, float volume)
    {
        audioMixer.SetFloat(parameter, Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
        PlayerPrefs.SetFloat(parameter, volume);
    }

    public float GetVolume(string parameter)
    {
        return PlayerPrefs.GetFloat(parameter, 1f); // valor por defecto
    }

    public void SetMute(bool isMuted)
    {
        if (isMuted)
        {
            audioMixer.SetFloat(MASTER_PARAM, -80f); // silencio total
        }
        else
        {
            float vol = GetVolume(MASTER_KEY);
            audioMixer.SetFloat(MASTER_PARAM, Mathf.Log10(Mathf.Clamp(vol, 0.0001f, 1f)) * 20f);
        }

        PlayerPrefs.SetInt(MUTE_KEY, isMuted ? 1 : 0);
    }

    public bool IsMuted()
    {
        return PlayerPrefs.GetInt(MUTE_KEY, 0) == 1;
    }

    private void LoadSettings()
    {
        SetVolume(MASTER_PARAM, GetVolume(MASTER_KEY));
        SetVolume(MUSIC_PARAM, GetVolume(MUSIC_KEY));
        SetVolume(SFX_PARAM, GetVolume(SFX_KEY));
        SetMute(IsMuted());
    }
}
