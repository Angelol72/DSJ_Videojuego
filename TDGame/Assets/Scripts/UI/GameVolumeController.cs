using UnityEngine;
using UnityEngine.UI;

public class GameVolumeController : MonoBehaviour
{
    public Slider masterVolumeSlider;

    private void Start()
    {
        // Initialize the slider value from AudioManager
        if (masterVolumeSlider != null)
        {
            float masterVol = AudioManager.Instance.GetVolume("mainVolume");
            masterVolumeSlider.value = masterVol;
            masterVolumeSlider.onValueChanged.AddListener((v) =>
            {
                AudioManager.Instance.SetVolume("mainVolume", v);
            });
        }
    }
}
