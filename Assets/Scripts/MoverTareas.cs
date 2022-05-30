 using UnityEngine;
 using System.Collections;

public class MoverTareas : MonoBehaviour
{
    private GameObject TareaSeleccionada;
    private Vector2 posInicial;
    Vector2 posDedo;

    private void Start()
    {
        TareaSeleccionada = null;
    }
    void Update()
    {

        //if(Input.touchCount > 0)
        //{
        //    switch (Input.GetTouch(0).phase)
        //    {
        //        case TouchPhase.Began:
        //            posDedo = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //            Collider2D targetObject = Physics2D.OverlapPoint(posDedo);
        //            if (targetObject)
        //            {
        //                TareaSeleccionada = targetObject.transform.gameObject;
        //                posInicial = TareaSeleccionada.transform.position;
        //            }
        //            break;
        //        case TouchPhase.Canceled:
        //            if(TareaSeleccionada != null)
        //            {
        //                TareaSeleccionada.transform.position = posInicial;
        //                TareaSeleccionada = null;
        //            }
        //            break;
        //        case TouchPhase.Ended:
        //            TareaSeleccionada = null;
        //            break;
        //        case TouchPhase.Moved:
        //            if(TareaSeleccionada != null)
        //            {
        //                posDedo = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //                TareaSeleccionada.transform.position = posDedo;
        //            }
        //            break;
        //    }
        //}

        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase.Equals(TouchPhase.Began)))
            {
                posDedo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D targetObject = Physics2D.OverlapPoint(posDedo);
                if (targetObject)
                {
                    TareaSeleccionada = targetObject.transform.gameObject;
                    posInicial = TareaSeleccionada.transform.position;
                    GameObject.Find("GameManager").GetComponent<GameManager>().QuitarTarea(TareaSeleccionada);
                }
            }
            else if (Input.GetMouseButtonDown(1) || (Input.touchCount > 0 && Input.GetTouch(0).phase.Equals(TouchPhase.Canceled)))
            {
                if (TareaSeleccionada != null)
                {
                    TareaSeleccionada.transform.position = posInicial;
                    TareaSeleccionada = null;
                }
            }
            //TODO checkear
            else if((Input.touchCount > 0 && !Input.GetTouch(0).phase.Equals(TouchPhase.Ended)) || Input.touchCount == 0)
            {
                if (TareaSeleccionada != null)
                {
                    posDedo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    TareaSeleccionada.transform.position = posDedo;
                }
            }
        }
        if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase.Equals(TouchPhase.Ended))) && TareaSeleccionada != null)
        {
            
            int columna  = (int)(TareaSeleccionada.transform.position.x + 7 - TareaSeleccionada.GetComponent<Tarea>().duracion / 2);
            int fila = (int)(TareaSeleccionada.transform.position.y + 3 );

            print("fila: " + fila + ", columna: " + columna);

            if (columna>=0 && columna+ (TareaSeleccionada.GetComponent<Tarea>().duracion-1) <= 13 && fila >=0 && fila <= 4 && gameObject.GetComponent<GameManager>().EsPosicionDisponible(TareaSeleccionada, fila, columna))
            {
                
                TareaSeleccionada.GetComponent<Tarea>().SetFilayColumna(fila, columna);
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().QuitarTarea(TareaSeleccionada);
                TareaSeleccionada.GetComponent<Tarea>().SetFilayColumna(-1, -1);
                TareaSeleccionada.GetComponent<Tarea>().ReiniciarPosicion();
            }
            TareaSeleccionada = null;
        }
    }




}

