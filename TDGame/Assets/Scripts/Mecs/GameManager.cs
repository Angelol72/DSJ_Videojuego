using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static bool GameOver { get; private set; }
    public static bool GamePaused { get; private set; }
    public static bool GameVictory { get; private set; }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        ResetGameOver();
        ResetGamePaused();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (!GameVictory && !GameOver)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (!GamePaused)
                    TriggerPausedTransition();
                else
                    ResumeGamePaused();
            }
        }
    }

    public void TriggerGameOverTransition()
    {
        if (GameOver || GamePaused || GameVictory) return; // Prevent multiple triggers

        GameOver = true;

        GameUISoundController.Instance.StopMusicWithFade(2f);
        UITransitionController.Instance.ActivateTransitionPanel();
        StartCoroutine(WaitAndShowDefeat());
    }

    public void TriggerVictoryTransition()
    {
        if (GamePaused || GameOver) return;

        GamePaused = true;
        GameVictory = true;

        GameUISoundController.Instance.StopMusicWithFade(2f);
        UITransitionController.Instance.ActivateTransitionPanel();
        StartCoroutine(WaitAndShowVictory());
    }

    public void TriggerPausedTransition()
    {
        if (GamePaused) return;

        GamePaused = true;

        UITransitionController.Instance.ActivatePauseWindow();
        Time.timeScale = 0f;
    }

    private void ResetGameOver()
    {
        GameOver = false;
    }

    private void ResetGamePaused()
    {
        GamePaused = false;
    }

    public void ResumeGamePaused()
    {
        ResetGamePaused();
        Time.timeScale = 1f;
        UITransitionController.Instance.DeactivatePauseWindow();
    }

    private System.Collections.IEnumerator WaitAndShowDefeat()
    {
        yield return new WaitForSecondsRealtime(1f);
        UITransitionController.Instance.ActivateGameOverWindow();
    }

    private System.Collections.IEnumerator WaitAndShowVictory()
    {
        yield return new WaitForSecondsRealtime(1f);
        UITransitionController.Instance.ActivateVictoryWindow();
    }
}