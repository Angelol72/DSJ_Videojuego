using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 180f; // Total time in seconds
    private float currentTime;
    public TMP_Text timerText; // Assign a TMP_Text in the inspector
    public Slider timerSlider; // Assign a UI Slider in the inspector
    public GameObject winCanvas; // Victory Canvas

    private bool isGameOver = false;

    void Start()
    {
        currentTime = totalTime;
        if (winCanvas != null)
            winCanvas.SetActive(false);

        // Initialize the slider
        if (timerSlider != null)
        {
            timerSlider.maxValue = totalTime;
            timerSlider.value = totalTime;
        }
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            WinGame();
        }
        UpdateTimerUI();
    }

    // Update the timer UI elements
    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();

        if (timerSlider != null)
            timerSlider.value = currentTime;
    }

    // Handle win condition when time runs out
    void WinGame()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        if (winCanvas != null)
            winCanvas.SetActive(true);
    }
}