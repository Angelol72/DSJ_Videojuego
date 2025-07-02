using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public AudioSource backgroundAudio; // Solo para esta escena
    public AudioSource clickAudio;      // Efecto de clic
    public GameObject iconoSonido;      // Icono cuando hay sonido
    public GameObject iconoMute;        // Icono cuando está muteado

    private void Start()
    {
        // Aplicar estado de mute y actualizar íconos
        UpdateMuteIcon();

        // Mutea backgroundAudio si corresponde
        if (backgroundAudio != null)
        {
            backgroundAudio.mute = AudioManager.Instance.IsMuted();
        }
    }

    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Mute()
    {
        bool nuevoEstadoMute = !AudioManager.Instance.IsMuted();
        AudioManager.Instance.SetMute(nuevoEstadoMute);

        // Aplica mute local si hay backgroundAudio
        if (backgroundAudio != null)
        {
            backgroundAudio.mute = nuevoEstadoMute;
        }

        UpdateMuteIcon();
    }

    public void Click()
    {
        if (clickAudio != null)
        {
            // Opcional: ajusta el volumen según configuración
            clickAudio.volume = AudioManager.Instance.GetVolume("fxVolume");
            clickAudio.Play();
        }
    }

    private void UpdateMuteIcon()
    {
        bool estaMuteado = AudioManager.Instance.IsMuted();

        if (iconoSonido != null) iconoSonido.SetActive(!estaMuteado);
        if (iconoMute != null) iconoMute.SetActive(estaMuteado);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
