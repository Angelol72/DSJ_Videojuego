using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerSimple : MonoBehaviour
{
    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

}