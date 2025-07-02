using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettingsMenu : MonoBehaviour
{
    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Textos de porcentaje")]
    public TextMeshProUGUI masterVolumeText;
    public TextMeshProUGUI musicVolumeText;
    public TextMeshProUGUI sfxVolumeText;

    [Header("Mute")]
    public Toggle muteToggle;

    private void Start()
    {
        // Inicializar valores desde AudioManager
        float masterVol = AudioManager.Instance.GetVolume("mainVolume");
        float musicVol = AudioManager.Instance.GetVolume("musicVolume");
        float sfxVol = AudioManager.Instance.GetVolume("fxVolume");

        masterSlider.value = masterVol;
        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;
        muteToggle.isOn = AudioManager.Instance.IsMuted();

        UpdateVolumeText(masterVolumeText, masterVol);
        UpdateVolumeText(musicVolumeText, musicVol);
        UpdateVolumeText(sfxVolumeText, sfxVol);

        // Asignar listeners automáticos
        masterSlider.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance.SetVolume("mainVolume", v);
            UpdateVolumeText(masterVolumeText, v);
        });

        musicSlider.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance.SetVolume("musicVolume", v);
            UpdateVolumeText(musicVolumeText, v);
        });

        sfxSlider.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance.SetVolume("fxVolume", v);
            UpdateVolumeText(sfxVolumeText, v);
        });

        muteToggle.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance.SetMute(v);
        });
    }

    private void UpdateVolumeText(TextMeshProUGUI text, float value)
    {
        if (text != null)
        {
            text.SetText("{0:0}%", Mathf.Clamp(value, 0.0001f, 1f) * 100f);
        }
    }
}
