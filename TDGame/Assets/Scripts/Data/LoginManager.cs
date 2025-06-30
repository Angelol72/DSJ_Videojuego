using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TMP_InputField inputApellido;
    public TMP_InputField inputGrado;

    public void GuardarDatosEnJson()
    {
        UsuarioData usuario = new UsuarioData();
        usuario.nombre = inputNombre.text;
        usuario.apellido = inputApellido.text;
        usuario.grado = inputGrado.text;

        string json = JsonUtility.ToJson(usuario, true);

        string ruta = Path.Combine(Application.persistentDataPath, "UsuarioData.json");
        File.WriteAllText(ruta, json);

        Debug.Log("Datos guardados en: " + ruta);
    }
}