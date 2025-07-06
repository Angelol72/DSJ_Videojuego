using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonNext : MonoBehaviour
{
    public string escenaASalir = "Niveles"; 
    public int nivelADesbloquear = 2; // Estamos desbloqueando el Nivel 2 (Medio)

    public void IrASelectorNivel()
    {
        // Guarda el progreso solo si el nuevo nivel es mayor al actual desbloqueado
        if (nivelADesbloquear > PlayerPrefs.GetInt("nivelesDesbloqueados", 1))
        {
            PlayerPrefs.SetInt("nivelesDesbloqueados", nivelADesbloquear);
        }

        // Carga la escena de selección de niveles
        SceneManager.LoadScene(escenaASalir);
    }
}

