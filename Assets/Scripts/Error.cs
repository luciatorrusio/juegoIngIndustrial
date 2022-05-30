using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Error 
{
    public int id { get; }
    public string mensaje { get;}
    public Error(int id)
    {
        this.id = id;
        mensaje = CompletarMensaje(id);
    }
    private string CompletarMensaje(int id)
    {
        switch (id)
        {
            case 0:
                return "Todo bien";
            case 1:
                return "Faltan tareas anteriores";
            case 2:
                return "No se colocaron todas las tareas";
            default:
                return "error no reconocido";
        }
    }
}
