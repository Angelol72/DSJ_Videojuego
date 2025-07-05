using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TMP_InputField inputApellido;
    public TMP_Dropdown dropdownGrado;

    private string rutaArchivo;

    private void Start()
    {
        rutaArchivo = Path.Combine(Application.persistentDataPath, "usuario.json");
    }

    public void GuardarDatosEnJson()
    {
        string nombre = inputNombre.text.Trim();
        string apellido = inputApellido.text.Trim();

        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido))
        {
            Debug.LogWarning("¡El nombre y el apellido son obligatorios!");
            return;
        }

        UsuarioData nuevoUsuario = new UsuarioData
        {
            nombre = nombre,
            apellido = apellido,
            grado = dropdownGrado.options[dropdownGrado.value].text,
			score = 0
        };
        string json = JsonUtility.ToJson(nuevoUsuario, true);
        File.WriteAllText(rutaArchivo, json);

        Debug.Log("Usuario guardado correctamente en: " + rutaArchivo);
		SceneManager.LoadScene("Choice");
    }
}