using UnityEngine;
using UnityEngine.UI;

public class PlayClickSound : MonoBehaviour
{
    public AudioClip clickSound; // arrastra el audio aqu�
    private AudioSource audioSource;

    private void Awake()
    {
        // Verifica si ya hay un AudioSource, si no, lo a�ade.
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    public void PlaySound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
