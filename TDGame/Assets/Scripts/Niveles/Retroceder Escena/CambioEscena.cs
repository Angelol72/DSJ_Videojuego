using UnityEngine;
using UnityEngine.SceneManagement;
public class CambioEscena : MonoBehaviour
{
    public string nombreEscenaDestino = "Choice";
    private AudioSource audioSource;

 

    public void ReproducirSonidoYCambiarEscena()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }

        Invoke("CargarEscena", 0.3f); // espera para dejar sonar el audio
    }

    private void CargarEscena()
    {
        SceneManager.LoadScene(nombreEscenaDestino);
    }
}