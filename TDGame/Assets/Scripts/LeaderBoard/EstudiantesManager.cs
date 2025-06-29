using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    public GameObject tablaEstudiantes; // El Panel con el Grid Layout Group
    public GameObject celdaPrefab;     // Prefab de un Text para las celdas
    private ListaEstudiantes listaEstudiantes;

    void Start()
    {
        CargarEstudiantes();
        CrearTabla();
    }

    void CargarEstudiantes()
    {
        TextAsset archivoJSON = Resources.Load<TextAsset>("estudiantes");
        if (archivoJSON != null)
        {
            listaEstudiantes = JsonUtility.FromJson<ListaEstudiantes>("{\"estudiantes\":" + archivoJSON.text + "}");
        }
        else
        {
            Debug.LogError("No se pudo cargar el archivo JSON.");
        }
    }

    void CrearTabla()
    {
        if (listaEstudiantes != null && listaEstudiantes.estudiantes.Length > 0)
        {
            var estudiantesOrdenados = listaEstudiantes.estudiantes
                .OrderByDescending(estudiante => estudiante.puntos)
                .ToArray();

       

            for (int i = 0; i < 5; i++)
            {
                var estudiante = estudiantesOrdenados[i];
                CrearCelda((i + 1).ToString());
                CrearCelda(estudiante.nombre);
                CrearCelda(estudiante.apellidos);
                CrearCelda(estudiante.grado);
                CrearCelda(estudiante.puntos.ToString());
            }
        }
    }

    void CrearCelda(string texto)
    {
        GameObject nuevaCelda = Instantiate(celdaPrefab, tablaEstudiantes.transform);
        nuevaCelda.GetComponent<Text>().text = texto;
    }
}
