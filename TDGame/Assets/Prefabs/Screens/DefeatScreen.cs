using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DefeatScreen : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI encouragementText;
    public TextMeshProUGUI hintText;
    public Button retryButton;
    public Button hintButton;
    public Button menuButton;
    public Image defeatIcon;

    [Header("Audio Clips")]
    public AudioClip defeatSound;           // Sonido suave de derrota
    public AudioClip encouragementSound;    // Sonido motivacional
    public AudioClip buttonClickSound;      // Sonido de botones
    public AudioClip whooshSound;          // Sonido de entrada
    
    [Header("Audio Settings")]
    [Range(0f, 1f)] public float sfxVolume = 0.7f;
    
    private AudioSource audioSource;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }
    
    void Start()
    {
        PlaySound(whooshSound);
        StartCoroutine(DefeatSequence());
    }

    void ConfigureButtons()
    {
        retryButton.onClick.AddListener(() => {
            PlaySound(buttonClickSound);
            RetryLevel();
        });
        
        menuButton.onClick.AddListener(() => {
            PlaySound(buttonClickSound);
            LoadMainMenu();
        });
    }
    
    IEnumerator DefeatSequence()
    {
        // 1. Animación de entrada suave
        yield return StartCoroutine(SoftScaleIn());
        
        yield return new WaitForSeconds(0.5f);
        
        // 2. Sonido suave de derrota
        PlaySound(defeatSound);
        
        yield return new WaitForSeconds(1f);
        
        // 3. Sonido motivacional
        PlaySound(encouragementSound);
        
        // 4. Animar mensaje motivacional
        if (encouragementText != null)
            StartCoroutine(PulseText(encouragementText));
    }
    
    IEnumerator SoftScaleIn()
    {
        transform.localScale = Vector3.zero;
        float time = 0;
        float duration = 0.6f;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;
            
            // Easing suave
            float scale = EaseOutQuart(progress);
            transform.localScale = Vector3.one * scale;
            
            yield return null;
        }
        
        transform.localScale = Vector3.one;
    }
    
    // Easing suave para efecto menos agresivo
    float EaseOutQuart(float t)
    {
        return 1f - Mathf.Pow(1f - t, 4f);
    }
    
    IEnumerator PulseText(TextMeshProUGUI text)
    {
        Vector3 originalScale = text.transform.localScale;
        
        for (int i = 0; i < 2; i++)
        {
            // Crecer
            float time = 0;
            while (time < 0.3f)
            {
                time += Time.deltaTime;
                float scale = Mathf.Lerp(1f, 1.1f, time / 0.3f);
                text.transform.localScale = originalScale * scale;
                yield return null;
            }
            
            // Volver al tamaño normal
            time = 0;
            while (time < 0.3f)
            {
                time += Time.deltaTime;
                float scale = Mathf.Lerp(1.1f, 1f, time / 0.3f);
                text.transform.localScale = originalScale * scale;
                yield return null;
            }
        }
        
        text.transform.localScale = originalScale;
    }
    
    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, sfxVolume);
        }
    }
    
    // Métodos públicos
    public void ShowDefeatScreen()
    {
        gameObject.SetActive(true);
    }
    
    public void ShowDefeatScreen(string customHint)
    {
        gameObject.SetActive(true);
        if (hintText != null)
        {
            hintText.text = "💡 " + customHint;
        }
    }
    
    // Métodos de botones
    public void RetryLevel()
    {
        Debug.Log("Reintentar nivel");
        // Tu lógica de retry aquí
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Debug.Log("Cargar menú principal");
        // Tu lógica de menú aquí
        SceneManager.LoadScene("Main Menu");
    }
}