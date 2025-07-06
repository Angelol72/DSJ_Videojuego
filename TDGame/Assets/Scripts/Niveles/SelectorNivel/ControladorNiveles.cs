using UnityEngine;
using UnityEngine.UI;

public class ControladorNiveles : MonoBehaviour
{
    public Button[] botonesNiveles;

    void Start()
    {
        // Desactiva todos
        for (int i = 0; i < botonesNiveles.Length; i++)
        {
            botonesNiveles[i].interactable = false;
        }

        // Activa según progreso guardado
        int nivelesDesbloqueados = PlayerPrefs.GetInt("nivelesDesbloqueados", 1);
        for (int i = 0; i < nivelesDesbloqueados; i++)
        {
            botonesNiveles[i].interactable = true;
        }
    }
}
