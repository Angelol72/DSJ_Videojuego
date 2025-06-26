using UnityEngine;

public class GameUISoundController : MonoBehaviour
{
    public static GameUISoundController Instance { get; private set; }

    public AudioClip openClip;
    public AudioClip closeClip;
    public AudioClip rage;
    public AudioClip correctAnswer;
    public AudioClip frozenSpell;
    public AudioClip lightningSpell;
    public AudioClip monsterDie;
    public AudioClip castleTakeDamage;
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayOpenSound()
    {
        if (audioSource != null && openClip != null)
            audioSource.PlayOneShot(openClip);
    }

    public void PlayCloseSound()
    {
        if (audioSource != null && closeClip != null)
            audioSource.PlayOneShot(closeClip);
    }

    public void PlayEnrageEnemy()
    {
        if (audioSource != null && closeClip != null)
            audioSource.PlayOneShot(rage);
    }

    public void PlayCorrectAnswer()
    {
        if (audioSource != null && closeClip != null)
            audioSource.PlayOneShot(correctAnswer);
    }

    public void PlayFrozenSpell()
    {
        if (audioSource != null && closeClip != null)
            audioSource.PlayOneShot(frozenSpell);
    }

    public void PlayLightningSpell()
    {
        if (audioSource != null && closeClip != null)
            audioSource.PlayOneShot(lightningSpell);
    }

    public void PlayMonsterDie()
    {
        if (audioSource != null && closeClip != null)
            audioSource.PlayOneShot(monsterDie);
    }

    public void PlayCastleTakeDamage()
    {
        if (audioSource != null && closeClip != null)
            audioSource.PlayOneShot(castleTakeDamage);
    }


}