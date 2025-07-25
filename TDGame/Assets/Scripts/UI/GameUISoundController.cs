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
    public AudioClip bgMusic;
    public AudioClip pauseMusic; 
    public AudioSource sfxSource;
    public AudioSource musicSource; // To play music bg
    public AudioSource musicSource2; // To play pause music
    private AudioClip currentMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentMusic = bgMusic;
        playCurrentMusic();
    }

    public void PlayOpenSound()
    {
        if (sfxSource != null && openClip != null)
            sfxSource.PlayOneShot(openClip);
    }

    public void PlayCloseSound()
    {
        if (sfxSource != null && closeClip != null)
            sfxSource.PlayOneShot(closeClip);
    }

    public void PlayEnrageEnemy()
    {
        if (sfxSource != null && closeClip != null)
            sfxSource.PlayOneShot(rage);
    }

    public void PlayCorrectAnswer()
    {
        if (sfxSource != null && closeClip != null)
            sfxSource.PlayOneShot(correctAnswer);
    }

    public void PlayFrozenSpell()
    {
        if (sfxSource != null && closeClip != null)
            sfxSource.PlayOneShot(frozenSpell);
    }

    public void PlayLightningSpell()
    {
        if (sfxSource != null && closeClip != null)
            sfxSource.PlayOneShot(lightningSpell);
    }

    public void PlayMonsterDie()
    {
        if (sfxSource != null && closeClip != null)
            sfxSource.PlayOneShot(monsterDie);
    }

    public void PlayCastleTakeDamage()
    {
        if (sfxSource != null && closeClip != null)
            sfxSource.PlayOneShot(castleTakeDamage);
    }

    public void changeCurrentMusicAndPlay(AudioClip music)
    {
        if (music != null)
        {
            currentMusic = music;
            playCurrentMusic();
        }
    }

    public void playCurrentMusic()
    {
        if (musicSource != null)
        {
            musicSource.clip = currentMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void StopMusicWithFade(float fadeDuration = 1f)
    {
        if (musicSource != null)
        {
            StartCoroutine(FadeOutMusic(fadeDuration));
        }
    }

    public void PlayPauseMusic()
    {
        if (musicSource2 != null && pauseMusic != null)
        {
            musicSource2.clip = pauseMusic;
            musicSource2.loop = true;
            musicSource2.Play();
        }
    }

    public void ResumePauseMusic()
    {
        if (musicSource2 != null)
        {
            if (!musicSource2.isPlaying)
                PlayPauseMusic();
            else
                musicSource2.UnPause();
        }
    }

    public void StopPauseMusic()
    {
        if (musicSource2 != null)
        {
            musicSource2.Pause();
        }
    }

    public void StopBGMusic()
    {
        if (musicSource != null)
        {
            musicSource.Pause();
        }
    }

    public void ResumeBGMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    private System.Collections.IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume; // Reset volume for next play
    }

}