using UnityEngine;
using System.IO;
using System.Linq;

public class Player : Unit
{

    public int score = 0; // Player's score

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Die()
    {
        if (state != UnitState.Dead)
            GameManager.Instance.TriggerGameOverTransition();

        state = UnitState.Dead;
    }

    public override void TakeDamage(int damage)
    {
        LifeController lifeManagement = GetComponent<LifeController>();
        // Sound effect on take damage
        GameUISoundController.Instance.PlayCastleTakeDamage();

        // Shake effect
        ShakeUnit shakeUnit = GetComponent<ShakeUnit>();
        if (shakeUnit != null)
        {
            shakeUnit.TriggerShake();
        }

        if (lifeManagement != null)
        {
            Unit.UnitState newState = lifeManagement.TakeDamage(damage);
            if (newState == Unit.UnitState.Dead)
            {
                Die(); // Call Die method if the player is dead
            }
        }
    }

    public void SaveScore()
    {
        string studentsFileName = "estudiantes.json";
        string userFileName = "usuario.json";
        string studentsPath = Path.Combine(Application.persistentDataPath, studentsFileName);
        string userDataPath = Path.Combine(Application.persistentDataPath, userFileName);

        // Read user data
        string userJson = File.Exists(userDataPath) ? File.ReadAllText(userDataPath) : null;
        string firstName = "John";
        string lastName = "Doe";
        string grade = "-1";
        if (!string.IsNullOrEmpty(userJson))
        {
            // Assumes UsuarioData has fields: nombre, apellidos, grado
            UsuarioData user = JsonUtility.FromJson<UsuarioData>(userJson);
            if (user != null)
            {
                firstName = user.nombre;
                lastName = user.apellido;
                grade = user.grado;
            }
        }

        // Read the existing estudiantes.json
        string json = File.Exists(studentsPath) ? File.ReadAllText(studentsPath) : "[]";

        // Adjust format to deserialize as ListaEstudiantes
        string wrappedJson = "{\"estudiantes\":" + json + "}";
        ListaEstudiantes studentsList = JsonUtility.FromJson<ListaEstudiantes>(wrappedJson);
        if (studentsList == null || studentsList.estudiantes == null)
            studentsList = new ListaEstudiantes { estudiantes = new Estudiante[0] };

        // Update or add student
        var newList = studentsList.estudiantes.ToList();
        var existing = newList.Find(e => e.nombre == firstName && e.apellidos == lastName && e.grado == grade);
        if (existing != null)
        {
            if (score > existing.puntos)
                existing.puntos = score;
        }
        else
        {
            Estudiante newStudent = new Estudiante {
                nombre = firstName,
                apellidos = lastName,
                grado = grade,
                puntos = score
            };
            newList.Add(newStudent);
        }
        studentsList.estudiantes = newList.ToArray();

        // Save back as a plain array
        string jsonArray = JsonUtility.ToJson(studentsList);
        int start = jsonArray.IndexOf('[');
        int end = jsonArray.LastIndexOf(']');
        string onlyArray = jsonArray.Substring(start, end - start + 1);
        File.WriteAllText(studentsPath, onlyArray);

    }
}
