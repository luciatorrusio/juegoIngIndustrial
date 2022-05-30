using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int numFilas;
    [SerializeField] private int numColumnas;
    [SerializeField] private int tiempoPorUnidad;
    private bool[,] matriz;
    private GameObject[,] tareasPosicion;
    private List<GameObject> tareasTerminadas;
    [SerializeField]private List<GameObject> tareasTodas;
    [SerializeField] private GameObject MenuError;
    [SerializeField] private GameObject ErrorText;
    [SerializeField] private GameObject FelicitacionesText;
    [SerializeField] private GameObject Tiempo;

    [SerializeField] private GameObject MenuFelicitaciones;
    
    private void Awake()
    {
        tiempoPorUnidad = 1;
        matriz = new bool[numFilas, numColumnas];// por default esta en false
        tareasTerminadas = new List<GameObject>();
        tareasPosicion = new GameObject[numFilas, numColumnas];
    }

    public bool EsPosicionDisponible(GameObject tarea, int f, int c)
    {
        int duracion = tarea.GetComponent<Tarea>().duracion;
        
        if (c + duracion > numColumnas)
            return false; // se sobrepasa del tiempo maximo.
        for (int i = 0; i < duracion; i++)
        {
            if (matriz[f, c + i]) return false;
        }
        
        return true;
    }

    private void printMatrix()
    {
        string aux = "";
        for (int i = 0; i < numFilas; i++)
        {
            
            for (int j = 0; j < numColumnas; j++)
            {
               aux+=matriz[i, j]==true? "1,":"0,";
            }
            aux += "\n";

        }
        print(aux);
    }

    public void AgregarTarea( GameObject tarea)
    {
        Tarea t = tarea.GetComponent<Tarea>();
        int fila = t.fila;
        int columna = t.columna;
        int duracion = t.duracion;

        if (columna + duracion > numColumnas)
            return; // se sobrepasa del tiempo maximo.
        
        
        
        for (int i =0; i < duracion; i++)
        {
            matriz[fila, i+columna] = true;
        }
        tareasPosicion[fila, columna] = tarea;
        printMatrix();
    }

    
    
    // supongo que me van a usar bien porque yo voy a codear el proyecto je
    public void QuitarTarea(GameObject tarea)
    {
        Tarea t = tarea.GetComponent<Tarea>();
       
        int fila = t.fila;
        int columna = t.columna;
        int duracion = t.duracion;
        if(fila >=0 && columna >=0){
            tareasPosicion[fila, columna] = null;
            for (int i = 0; i < duracion; i++)
            {
                matriz[fila, i+columna] = false;
            }
        }
        
        
    }


    public void IniciarPrueba()
    {
        StartCoroutine(CorrerColumna(0));
    }

    private IEnumerator CorrerColumna(int c)
    {
        Error e = new Error(0);
        if (c == numColumnas)
        {
            // termino de correr
            if (ContainsAll(tareasTerminadas, tareasTodas))
            {
                GameObject ultima =  tareasTerminadas[tareasTerminadas.Count-1];
                int t = ultima.GetComponent<Tarea>().duracion + ultima.GetComponent<Tarea>().columna;
                DetenerTiempo();
                GameObject.Find("Tiempo").GetComponent<MoverTiempo>().TiempoParado();
                MenuFelicitaciones.SetActive(true);
                FelicitacionesText.GetComponent<TextMeshProUGUI>().text = "Tiempo utilizado: \n" + t.ToString() + " días";
                yield return new WaitForSeconds(0);
            }
            else
            {
                e = new Error(2);
                DetenerTiempo();
                GameObject.Find("Tiempo").GetComponent<MoverTiempo>().TiempoParado();
                MenuError.SetActive(true);
                ErrorText.GetComponent<TextMeshProUGUI>().text = e.mensaje;
                yield return new WaitForSeconds(0);
            }
        }
        else
        {
            GameObject[] columna = GetColumn(tareasPosicion, c);
            foreach (GameObject tarea in columna)
            {
                e = tarea != null ? tarea.GetComponent<Tarea>().IniciarTarea(tareasTerminadas) : e;
                if (e.id != 0) break;
            }
            if (e.id == 0)
            {
                yield return new WaitForSeconds(tiempoPorUnidad);
                StartCoroutine(CorrerColumna(c + 1));
            }
            else
            {
                DetenerTiempo();
                Tiempo.GetComponent<MoverTiempo>().TiempoParado();
                MenuError.SetActive(true);
                // do something with the error
                ErrorText.GetComponent<TextMeshProUGUI>().text = e.mensaje;
            }
        }
        
        
        
    
    }
    public bool ContainsAll(List<GameObject> list, List<GameObject> objectsNeeded)
    {
        foreach (GameObject obj in objectsNeeded)
        {
            if (!list.Contains(obj)) 
                return false;
        }
        return true;
    }

    public void DetenerTiempo()
    {
        //Time.timeScale = 0;
    }
    public void ReanudarTiempo()
    {
        //Time.timeScale = 1;
    }

    public void NuevaPartida()
    {
        tareasTerminadas = new List<GameObject>();
        matriz = new bool[numFilas, numColumnas];
        tareasPosicion = new GameObject[numFilas, numColumnas];
        // mostrar indicaciones blabla
        foreach (GameObject tarea in tareasTodas)
        {
            tarea.GetComponent<Tarea>().ReiniciarPosicion();
        }
    }
    public void mejorarModelo()
    {
        tareasTerminadas = new List<GameObject>();
        
    }


    public GameObject[] GetColumn(GameObject[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public Tarea[] GetRow(Tarea[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }

    public void AgregarTareaTerminada(GameObject tarea)
    {
        tareasTerminadas.Add(tarea);
    }





}
