using UnityEngine;

public class UITransitionController : MonoBehaviour
{
    public static UITransitionController Instance { get; private set; }

    [SerializeField] private GameObject transitionPanel;
    [SerializeField] private GameObject defeatWindow;
    [SerializeField] private GameObject victoryWindow;
    [SerializeField] private GameObject pauseWindow;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Call this method to activate the transition panel
    public void ActivateTransitionPanel()
    {
        if (transitionPanel != null)
            transitionPanel.SetActive(true);
    }

    public void ActivateGameOverWindow()
    {
        if (defeatWindow != null)
            defeatWindow.SetActive(true);
    }

    public void ActivateVictoryWindow()
    {
        if (victoryWindow != null)
            victoryWindow.SetActive(true);
    }

    public void ActivatePauseWindow()
    {
        if (pauseWindow != null)
            pauseWindow.SetActive(true);
    }

    public void DeactivatePauseWindow()
    {
        if (pauseWindow != null)
            pauseWindow.SetActive(false);
    }

}
