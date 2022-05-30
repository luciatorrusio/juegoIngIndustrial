using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarea : MonoBehaviour
{
    [SerializeField] private List<GameObject> tareasInmediatamenteAnterioresNecesarias;
    [SerializeField] public int duracion;
    [SerializeField] public int fila;
    [SerializeField] public int columna;
    [SerializeField] private float posInicialx;
    [SerializeField] private float posInicialy;

    public GameManager GM;


    private void Start()
    {
        fila = -1;
        columna = -1;
        EscalarTamano();
    }

    private void EscalarTamano()
    {
        BoxCollider2D m_Collider = GetComponent<BoxCollider2D>();

        GameObject child = gameObject.transform.Find("Sprite").gameObject;
        Vector3 nuevaEscala = new Vector3(child.transform.localScale.x * duracion, child.transform.localScale.y, child.transform.localScale.z);
        m_Collider.size = new Vector3(m_Collider.size.x * duracion, m_Collider.size.y);
        child.transform.localScale = nuevaEscala;

        GameObject text = gameObject.transform.Find("Canvas").Find("Text").gameObject;

        RectTransform r = text.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(duracion, r.sizeDelta.y);
    }

    public void ReiniciarPosicion()
    {
        transform.localPosition = new Vector3(posInicialx, posInicialy);
    }

    public bool ContainsAll(List<GameObject> list, List<GameObject> objectsNeeded)
    {
        foreach (GameObject obj in objectsNeeded)
        {
            if (!list.Contains(obj)) return false;
        }
        return true;
    }

    public Error IniciarTarea(List<GameObject> tareasTerminadas)
    {
        Error e;
        if (ContainsAll(tareasTerminadas, tareasInmediatamenteAnterioresNecesarias))
        {
            IniciarAnimacion();
            StartCoroutine(TareaTerminada());
            e = new Error(0);
        }
        else
        {
            e = new Error(1);
        }
        return e;
        
    }

    private IEnumerator TareaTerminada()
    {
        yield return new WaitForSeconds(duracion);
        DetenerAnimacion();
        GM.AgregarTareaTerminada(gameObject);
    }

    private void IniciarAnimacion()
    {
        print("inicio animacion");
        // iniciar animacion
    }

    private void DetenerAnimacion()
    {
        print("detengo aniacion");
        // detener animacion
    }


    
    public void SetFilayColumna(int fila, int columna)
    {
        if(fila >= 0 && columna >=0){
            GM.QuitarTarea(gameObject);
            this.fila = fila;
            this.columna = columna;
            transform.position = new Vector3(columna-7f+((float)duracion)/2, fila - 2.5f, transform.position.z);
            //transform.position = new Vector3(columna - 7f , fila - 2.5f, transform.position.z);
            GM.AgregarTarea(gameObject);
        }
        this.fila = fila;
        this.columna = columna;
    }







}
