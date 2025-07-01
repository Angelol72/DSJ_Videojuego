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
        rutaArchivo = Path.Combine(Application.persistentDataPath, "usuarios.json");
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
            grado = dropdownGrado.options[dropdownGrado.value].text
        };

        ListaUsuarios lista;

        // Leer archivo existente
        if (File.Exists(rutaArchivo))
        {
            string jsonExistente = File.ReadAllText(rutaArchivo);
            lista = JsonUtility.FromJson<ListaUsuarios>(jsonExistente);
            if (lista == null) lista = new ListaUsuarios();
        }
        else
        {
            lista = new ListaUsuarios();
        }

        // Agregar nuevo usuario a la lista
        lista.usuarios.Add(nuevoUsuario);

        // Guardar la lista completa otra vez
        string jsonFinal = JsonUtility.ToJson(lista, true);
        File.WriteAllText(rutaArchivo, jsonFinal);

        Debug.Log("Usuario agregado correctamente: " + rutaArchivo);
		SceneManager.LoadScene("Choice");
    }
}
