using UnityEngine;

public class ResetProgresoAlInicio : MonoBehaviour
{
    private void Awake()
    {
        // Esto solo se ejecuta cuando entras por primera vez al juego
        if (!PlayerPrefs.HasKey("progresoInicializado"))
        {
            PlayerPrefs.DeleteKey("nivelesDesbloqueados"); // Opcional: o ponlo a 1
            PlayerPrefs.SetInt("nivelesDesbloqueados", 1); // Solo bot�n F�cil habilitado
            PlayerPrefs.SetInt("progresoInicializado", 1); // Ya no lo volver� a hacer
            PlayerPrefs.Save();
        }
    }
}
