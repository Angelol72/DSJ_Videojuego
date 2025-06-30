using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public AudioSource backgroundAudio;
	public AudioSource clickAudio;
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
	
	public void Click()
	{
		clickAudio.Play();
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