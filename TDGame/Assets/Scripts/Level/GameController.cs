using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas; // Assign the pause Canvas from the Inspector

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false); // Hide the pause Canvas at the start
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Escape key was pressed this frame using the new Input System
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
        if (pauseCanvas != null)
            pauseCanvas.SetActive(true); // Show the pause Canvas
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false); // Hide the pause Canvas
    }
}
