using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBarController : MonoBehaviour
{

    public Slider manaSlider;
    public TMP_Text textMana;

    void Update()
    {
        ManaController manaController = GetComponent<ManaController>();
        if (manaController == null)
        {
            return;
        }

        textMana?.SetText($"{manaController.currentMana} / {manaController.maxMana}");


        if (manaSlider != null)
        {
            manaSlider.maxValue = manaController.maxMana;
            manaSlider.value = manaController.currentMana;
        }
    }
}