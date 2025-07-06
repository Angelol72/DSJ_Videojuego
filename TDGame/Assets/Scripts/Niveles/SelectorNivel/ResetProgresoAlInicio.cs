using UnityEngine;

public class ResetProgresoAlInicio : MonoBehaviour
{
    private void Awake()
    {
        // Siempre se reinicia el progreso a 1 (solo nivel fácil habilitado)
        PlayerPrefs.SetInt("nivelesDesbloqueados", 1);
        PlayerPrefs.Save();
    }
}
