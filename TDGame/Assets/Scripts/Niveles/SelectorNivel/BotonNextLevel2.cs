using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonNextLevel2 : MonoBehaviour
{
    public string escenaASalir = "Niveles"; 
    public int nivelADesbloquear = 3;      // Nivel que se desbloquear�

    public void IrASelectorNivel()
    {
        // Si el nivel a desbloquear es mayor al actual guardado, lo actualizamos
        if (nivelADesbloquear > PlayerPrefs.GetInt("nivelesDesbloqueados", 1))
        {
            PlayerPrefs.SetInt("nivelesDesbloqueados", nivelADesbloquear);
        }

        // Vamos a la escena de selecci�n de niveles
        SceneManager.LoadScene(escenaASalir);
    }
}
