using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambioEscena(string nombreDeEscena)
    {
        SceneManager.LoadScene(nombreDeEscena);
    }
}