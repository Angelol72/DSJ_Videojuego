using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Button muteButton;
    public Image icon;
    public Sprite mutedSprite;
    public Sprite unmutedSprite;

    private void Start()
    {
        muteButton.onClick.AddListener(ToggleMute);
        UpdateIcon();
    }

    private void ToggleMute()
    {
        bool current = AudioManager.Instance.IsMuted();
        AudioManager.Instance.SetMute(!current);
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (icon != null)
        {
            icon.sprite = AudioManager.Instance.IsMuted() ? mutedSprite : unmutedSprite;
        }
    }
}
