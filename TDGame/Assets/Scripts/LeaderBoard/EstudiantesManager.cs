using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

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
    [Header("Referencias UI")]
    public GameObject tablaEstudiantes;     // El Panel con el Grid Layout Group (dentro del ScrollRect)
    public GameObject celdaPrefab;          // Prefab de un Text para las celdas
    public ScrollRect scrollRect;           // Referencia al ScrollRect
    
    [Header("Configuración")]
    public bool incluirEncabezados = true;  // Si quieres mostrar encabezados
    
    private ListaEstudiantes listaEstudiantes;
    
    void Start()
    {
        CargarEstudiantes();
        CrearTabla();
    }
    
    void CargarEstudiantes()
    {
        // Ruta persistente para estudiantes.json
        string studentsPath = Path.Combine(Application.persistentDataPath, "estudiantes.json");
        if (File.Exists(studentsPath))
        {
            string json = File.ReadAllText(studentsPath);
            // Envuelve el array para deserializar correctamente
            listaEstudiantes = JsonUtility.FromJson<ListaEstudiantes>("{\"estudiantes\":" + json + "}");
        }
        else
        {
            Debug.LogWarning("No se encontró estudiantes.json en persistentDataPath. Se crea lista vacía.");
            listaEstudiantes = new ListaEstudiantes { estudiantes = new Estudiante[0] };
        }
    }
    
    void CrearTabla()
    {
        if (listaEstudiantes != null && listaEstudiantes.estudiantes.Length > 0)
        {
            // Limpiar tabla existente
            LimpiarTabla();
            
            // Crear encabezados si está habilitado
            if (incluirEncabezados)
            {
                CrearEncabezados();
            }
            
            // Ordenar estudiantes por puntos
            var estudiantesOrdenados = listaEstudiantes.estudiantes
                .OrderByDescending(estudiante => estudiante.puntos)
                .ToArray();
            
            // Crear filas para TODOS los estudiantes (sin límite)
            for (int i = 0; i < estudiantesOrdenados.Length; i++)
            {
                var estudiante = estudiantesOrdenados[i];
                CrearFilaEstudiante(i + 1, estudiante);
            }
            
            // Ajustar el contenido del scroll
            AjustarContenidoScroll();
        }
    }
    
    void CrearEncabezados()
    {
        CrearCelda("Pos.", true);
        CrearCelda("Nombre", true);
        CrearCelda("Apellidos", true);
        CrearCelda("Grado", true);
        CrearCelda("Puntos", true);
    }
    
    void CrearFilaEstudiante(int posicion, Estudiante estudiante)
    {
        CrearCelda(posicion.ToString());
        CrearCelda(estudiante.nombre);
        CrearCelda(estudiante.apellidos);
        CrearCelda(estudiante.grado);
        CrearCelda(estudiante.puntos.ToString());
    }
    
    void CrearCelda(string texto, bool esEncabezado = false)
    {
        GameObject nuevaCelda = Instantiate(celdaPrefab, tablaEstudiantes.transform);
        Text textComponent = nuevaCelda.GetComponent<Text>();
        textComponent.text = texto;
        
        // Opcional: Estilo diferente para encabezados
        if (esEncabezado)
        {
            textComponent.fontStyle = FontStyle.Bold;
            textComponent.color = Color.black;
        }
    }
    
    void LimpiarTabla()
    {
        // Destruir todas las celdas existentes
        foreach (Transform child in tablaEstudiantes.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    void AjustarContenidoScroll()
    {
        // Forzar la actualización del layout
        Canvas.ForceUpdateCanvases();
        
        // Opcional: Hacer scroll hacia arriba
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }
    
    // Método público para recargar la tabla
    public void RecargarTabla()
    {
        CargarEstudiantes();
        CrearTabla();
    }
    
    // Método para ir al principio de la lista
    public void IrAlPrincipio()
    {
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }
    
    // Método para ir al final de la lista
    public void IrAlFinal()
    {
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}