using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
	public AudioSource backgroundAudio;
    
	public void CambiarEscena(string nombre)
	{
		SceneManager.LoadScene(nombre);
	}
	
	public void Mute()
	{
		if(backgroundAudio != null)
		{
			backgroundAudio.mute = !backgroundAudio.mute;
		}
	}
	
	
}
