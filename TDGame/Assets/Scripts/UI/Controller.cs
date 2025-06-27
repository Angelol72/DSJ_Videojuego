using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public AudioSource backgroundAudio;
    public GameObject iconoSonido; 
    public GameObject iconoMute;   

    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Mute()
    {
        if (backgroundAudio != null)
        {
            backgroundAudio.mute = !backgroundAudio.mute;
            bool estaMuteado = backgroundAudio.mute;
            if (iconoSonido != null) iconoSonido.SetActive(!estaMuteado);
            if (iconoMute != null) iconoMute.SetActive(estaMuteado);
        }
    }
}