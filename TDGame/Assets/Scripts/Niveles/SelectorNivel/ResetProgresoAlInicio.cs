using UnityEngine;

public class ResetProgresoAlInicio : MonoBehaviour
{
    private void Awake()
    {
        // Siempre se reinicia el progreso a 1 (solo nivel f�cil habilitado)
        PlayerPrefs.SetInt("nivelesDesbloqueados", 1);
        PlayerPrefs.Save();
    }
}
