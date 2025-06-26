using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellUIController : MonoBehaviour
{
    public Slider lightningSlider;
    public TMP_Text manaTextLightning;
    public Slider freezeSlider;
    public TMP_Text manaTextFreeze;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // code to set mana test for lightning and freeze spells
            SpellController spellController = player.GetComponent<SpellController>();
            if (spellController != null)
            {
                manaTextLightning.SetText($"{spellController.lightningManaCost}");
                manaTextFreeze.SetText($"{spellController.freezeManaCost}");
            }
        }
        
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SpellController spellController = player.GetComponent<SpellController>();
            ManaController manaController = player.GetComponent<ManaController>();

            float value = manaController.currentMana;

            if (spellController != null)
            {
                // Update the lightning slider
                if (lightningSlider != null)
                {
                    lightningSlider.maxValue = spellController.lightningManaCost;
                    lightningSlider.value = lightningSlider.maxValue - value;
                }

                // Update the freeze slider
                if (freezeSlider != null)
                {
                    freezeSlider.maxValue = spellController.freezeManaCost;
                    freezeSlider.value = freezeSlider.maxValue - value;
                }
            }
        }
        
    }
}
