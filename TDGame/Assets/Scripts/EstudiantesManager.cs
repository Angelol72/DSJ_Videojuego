using UnityEngine;
using UnityEngine.UI;
using System.Linq; // Para usar LINQ y ordenar la lista

[System.Serializable]
public class Estudiante
{
    public string nombre;
    public string apellidos;
    public string grado;
    public int puntos;
}

[System.Serializable]
public class ListaEstudiantes
{
    public Estudiante[] estudiantes;
}

public class EstudiantesManager : MonoBehaviour
{
    public Text estudiantesText; // Arrastra un componente de texto aquí desde el editor

    private ListaEstudiantes listaEstudiantes;

    void Start()
    {
        CargarEstudiantes();
        MostrarEstudiantesOrdenados();
    }

    void CargarEstudiantes()
    {
        // Cargar el archivo JSON desde la carpeta Resources
        TextAsset archivoJSON = Resources.Load<TextAsset>("estudiantes");

        if (archivoJSON != null)
        {
            // Parsear el JSON y convertirlo a objetos C#
            listaEstudiantes = JsonUtility.FromJson<ListaEstudiantes>("{\"estudiantes\":" + archivoJSON.text + "}");
            Debug.Log("Archivo JSON cargado correctamente.");
        }
        else
        {
            Debug.LogError("No se pudo cargar el archivo JSON.");
        }
    }

    void MostrarEstudiantesOrdenados()
    {
        if (listaEstudiantes != null && listaEstudiantes.estudiantes.Length > 0)
        {
            // Ordenar estudiantes por puntos en orden descendente
            var estudiantesOrdenados = listaEstudiantes.estudiantes
                .OrderByDescending(estudiante => estudiante.puntos)
                .ToArray();

            string textoAMostrar = "";

            // Generar la lista con el puesto
            for (int i = 0; i < estudiantesOrdenados.Length; i++)
            {
                var estudiante = estudiantesOrdenados[i];
                textoAMostrar += $"{i + 1}. {estudiante.nombre} {estudiante.apellidos}, Grado: {estudiante.grado}, Puntos: {estudiante.puntos}\n";
            }

            estudiantesText.text = textoAMostrar;
        }
        else
        {
            estudiantesText.text = "No hay estudiantes para mostrar.";
        }
    }
}
