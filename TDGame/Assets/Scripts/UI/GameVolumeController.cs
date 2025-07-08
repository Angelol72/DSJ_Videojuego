using UnityEngine;
using UnityEngine.UI;

public class GameVolumeController : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Button muteButton;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;
    public Image muteButtonImage;
    private bool isMuted = false;
    private float lastVolume = 1f;

    private void Start()
    {
        // Get the latest values from AudioManager
        float masterVol = AudioManager.Instance.GetVolume("mainVolume");
        bool muted = AudioManager.Instance.IsMuted();

        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = masterVol;
            lastVolume = masterVol > 0.01f ? masterVol : 1f;
            isMuted = muted;
            masterVolumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
        if (muteButton != null)
        {
            muteButton.onClick.AddListener(ToggleMute);
        }
        UpdateMuteIcon();
    }

    private void OnSliderValueChanged(float value)
    {
        isMuted = AudioManager.Instance.IsMuted();
        AudioManager.Instance.SetVolume("mainVolume", value);
        if (value > 0.01f)
            lastVolume = value;
        UpdateMuteIcon();
    }

    private void ToggleMute()
    {
        isMuted = !isMuted;
        AudioManager.Instance.SetMute(isMuted);
        UpdateMuteIcon();
    }

    private void UpdateMuteIcon()
    {
        if (muteButtonImage != null)
        {
            if (isMuted || masterVolumeSlider.value <= 0.01f)
                muteButtonImage.sprite = soundOffIcon;
            else
                muteButtonImage.sprite = soundOnIcon;
        }
    }
}
