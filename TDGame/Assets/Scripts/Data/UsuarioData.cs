using System;
using System.Collections.Generic;

[Serializable]
public class UsuarioData
{
    public string nombre;
    public string apellido;
    public string grado;
    public int score; 
}

[Serializable]
public class ListaUsuarios
{
    public List<UsuarioData> usuarios = new List<UsuarioData>();
}