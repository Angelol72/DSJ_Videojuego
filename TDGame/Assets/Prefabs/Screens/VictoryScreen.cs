using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class VictoryScreen : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI messageText;
    public Image[] stars;
    public Button nextLevelButton;
    public Button retryButton;
    public Button menuButton;
    
    [Header("Audio Clips")]
    public AudioClip victoryFanfare;
    public AudioClip starSound;
    public AudioClip buttonClickSound;
    public AudioClip whooshSound;
    
    [Header("Particle Effects")]
    public ParticleSystem confettiParticles;
    
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
        ConfigureUI();
        ConfigureButtons();
        PlaySound(whooshSound);
        StartCoroutine(VictorySequence());
    }
    
    void ConfigureButtons()
    {
        nextLevelButton.onClick.AddListener(() => {
            PlaySound(buttonClickSound);
            LoadNextLevel();
        });
        
        retryButton.onClick.AddListener(() => {
            PlaySound(buttonClickSound);
            RetryLevel();
        });
        
        menuButton.onClick.AddListener(() => {
            PlaySound(buttonClickSound);
            LoadMainMenu();
        });
    }
    
    IEnumerator VictorySequence()
    {
        // 1. Animación de entrada SIN LeanTween
        yield return StartCoroutine(ScaleInAnimation());
        
        yield return new WaitForSeconds(0.3f);
        
        // 2. Sonido principal
        PlaySound(victoryFanfare);
        
        // 3. Partículas
        if (confettiParticles != null)
            confettiParticles.Play();
        
        yield return new WaitForSeconds(0.5f);
        
        // 4. Estrellas
        yield return StartCoroutine(AnimateStarsWithSound());
    }
    
    // Animación de escala SIN LeanTween
    IEnumerator ScaleInAnimation()
    {
        transform.localScale = Vector3.zero;
        float time = 0;
        float duration = 0.5f;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;
            
            // Efecto bounce (similar a LeanTween)
            float scale = BounceEaseOut(progress);
            transform.localScale = Vector3.one * scale;
            
            yield return null;
        }
        
        transform.localScale = Vector3.one;
    }
    
    // Función para efecto bounce
    float BounceEaseOut(float t)
    {
        if (t < 1f / 2.75f)
        {
            return 7.5625f * t * t;
        }
        else if (t < 2f / 2.75f)
        {
            return 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f;
        }
        else if (t < 2.5f / 2.75f)
        {
            return 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f;
        }
        else
        {
            return 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
        }
    }
    
    IEnumerator AnimateStarsWithSound()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] != null)
            {
                // Animar estrella SIN LeanTween
                yield return StartCoroutine(StarBounceAnimation(stars[i]));
                PlaySound(starSound);
                yield return new WaitForSeconds(0.4f);
            }
        }
    }
    
    // Animación de estrella SIN LeanTween
    IEnumerator StarBounceAnimation(Image star)
    {
        star.transform.localScale = Vector3.zero;
        star.color = Color.yellow;
        
        float time = 0;
        float duration = 0.3f;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;
            float scale = BounceEaseOut(progress);
            star.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        
        star.transform.localScale = Vector3.one;
    }
    
    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, sfxVolume);
        }
    }
    
    public void ShowVictoryScreen()
    {
        gameObject.SetActive(true);
    }
    
    public void ShowVictoryScreen(int starsEarned)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].gameObject.SetActive(i < starsEarned);
        }
    }
    
    void LoadNextLevel()
    {
        Debug.Log("Siguiente nivel");
    }
    
    void RetryLevel()
    {
        Debug.Log("Retry level");
    }
    
    void LoadMainMenu()
    {
        Debug.Log("Main menu");
    }
    
    void ConfigureUI()
    {
        if (titleText != null)
        {
            titleText.text = "¡VICTORIA!";
            titleText.color = Color.green;
        }
        
        if (messageText != null)
        {
            messageText.text = "¡Excelente trabajo!\nHas completado el nivel exitosamente.";
        }
    }
}